using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AugustCodingExercise.Interfaces;
using BusLogicLayer;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AugustCodingExercise
{
    public class PayrollRunsController : Controller
    {
        private readonly PayrollDBContext _context;
        private readonly PeopleDBContext _peopleContext;
        private readonly IDeductionCalc _deductionCalcService;
        private readonly IDiscountCalc _discountCalcService;
        private readonly CalcDBContext _calcContext;
        private readonly IPersonRepository _personRepo;

        public PayrollRunsController(IPersonRepository personRepo, PayrollDBContext context, PeopleDBContext peopleContext, CalcDBContext calcContext, IDeductionCalc deductionCalcService, IDiscountCalc discountCalcService)
        {
            _personRepo = personRepo;
            _context = context;
            _calcContext = calcContext;
            _peopleContext = peopleContext;
            _deductionCalcService = deductionCalcService;
            _discountCalcService = discountCalcService;
        }

        // GET: PayrollRuns
        public async Task<IActionResult> Index()
        {
            return View(await _context.PayrollRuns.ToListAsync());
        }

        // GET: PayrollRuns/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollRuns = await _context.PayrollRuns
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payrollRuns == null)
            {
                return NotFound();
            }

            return View(payrollRuns);
        }

        // GET: PayrollRuns/Create
        public IActionResult Create()
        {
            //TODO: make this data-driven via a new Defaults table in the database. Add a new C# SetDefaultData Service
            ViewData["DiscountType"] = "NAM";
            ViewData["DeductionType"] = "BEN";

            ViewData["PersonId"] = 
                new SelectList((from s in _peopleContext.Person.ToList()
                    select new
                    {
                        PersonId = s.PersonId,
                        FullName = s.PersonId + " " + s.FirstName + " " + s.LastName
                    }), "PersonId","FullName");

            return View();
        }

        // POST: PayrollRuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PayrollId,PersonId,RunId,WeekEnding,Status,CheckDate,GrossPay,Deduction,DeductionType,Discount,DiscountType,NetPay,Year")] PayrollRuns payrollRuns)
        {
            Person p = null;

            if (ModelState.IsValid)
            {
                p = await _personRepo.FindPerson(payrollRuns.PersonId);

                payrollRuns.GrossPay = p.Salary;
                List<DeductionType> listOfDeductionTypes = await _calcContext.Set<DeductionType>().ToListAsync();
                List<DiscountType> listOfDiscountTypes = await _calcContext.Set<DiscountType>().Where<DiscountType>(d => d.DiscountTypeCode == "NAM").ToListAsync();

                decimal? grossDeduction = _deductionCalcService.CalcDeduction(p, listOfDeductionTypes);
                decimal? discountAmt = _discountCalcService.CalcDiscount(p, grossDeduction, listOfDiscountTypes);
                payrollRuns.Discount = discountAmt;

                decimal? netDeduction = grossDeduction - discountAmt;
                payrollRuns.Deduction = netDeduction;

                payrollRuns.NetPay = payrollRuns.GrossPay - payrollRuns.Deduction;

                _context.Add(payrollRuns);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PersonId"] =
                new SelectList((from s in _peopleContext.Person.ToList()
                            select new
                            {
                                PersonId = s.PersonId,
                                FullName = s.PersonId + " " + s.FirstName + " " + s.LastName
                            }), "PersonId", "FullName", p.PersonId);

            return View(payrollRuns);
        }

        // GET: PayrollRuns/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollRuns = await _context.PayrollRuns.FindAsync(id);
            if (payrollRuns == null)
            {
                return NotFound();
            }
            return View(payrollRuns);
        }

        // POST: PayrollRuns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PayrollId,PersonId,RunId,WeekEnding,Status,CheckDate,GrossPay,Deduction,DeductionType,Discount,DiscountType,NetPay,Year")] PayrollRuns payrollRuns)
        {
            if (id != payrollRuns.PayrollId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payrollRuns);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PayrollRunsExists(payrollRuns.PayrollId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(payrollRuns);
        }

        // GET: PayrollRuns/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payrollRuns = await _context.PayrollRuns
                .FirstOrDefaultAsync(m => m.PayrollId == id);
            if (payrollRuns == null)
            {
                return NotFound();
            }

            return View(payrollRuns);
        }

        // POST: PayrollRuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var payrollRuns = await _context.PayrollRuns.FindAsync(id);
            _context.PayrollRuns.Remove(payrollRuns);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PayrollRunsExists(long id)
        {
            return _context.PayrollRuns.Any(e => e.PayrollId == id);
        }
    }
}

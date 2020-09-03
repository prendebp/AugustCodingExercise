using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AugustCodingExercise
{
    public class DiscountCalcService : IDiscountCalc
    {
        private readonly CalcDBContext _calcContext;

        public DiscountCalcService(CalcDBContext calcContext)
        {
            _calcContext = calcContext;
        }

        public decimal? RetrieveDiscountRate(Person p)
        {
            DiscountType dt = null;
            decimal? deductionAmt = 0.00M;

            //TODO: consider refactoring to the Strategy design pattern here. Make each a class.
            if (p.PersonCode == "EMP")
            {
                dt = _calcContext.DiscountType.Find("NAM");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.EmpPercent;
                }
            }

            if (p.PersonCode == "DEP")
            {
                dt = _calcContext.DiscountType.Find("NAM");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.DepPercent;
                }
            }

            if (p.PersonCode == "SPO")
            {
                dt = _calcContext.DiscountType.Find("NAM");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.SpoPercent;
                }
            }

            return deductionAmt;
        }

        public decimal? CalcDiscount(Person p,decimal? grossDeduction)
        {
            return grossDeduction * RetrieveDiscountRate(p);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AugustCodingExercise;
//using AugustCodingExercise.Interfaces;

namespace AugustCodingExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IPersonRepository _repo;

        public HomeController(ILogger<HomeController> logger) //, IPersonRepository repo)
        {
            _logger = logger;
            //_repo = repo;
        }

        public IActionResult Index()
        {
            //var result = await _repo.GetPeople();
            //string output = string.Empty;
            //foreach (Person item in result)
            //{
            //    output = item.FirstName.ToString() + " " + ((item?.MiddleName?.ToString())??"") + " " + item.LastName.ToString();

            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

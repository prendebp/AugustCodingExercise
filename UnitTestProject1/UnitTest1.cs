using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AugustCodingExercise;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels;
using BusLogicLayer;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCalcDeduction()
        {
            long personId = 7;//Test a dependent

            List<Person> stringList = new List<Person>
            {
                new Person() { Honorific = "Mr.", FirstName = "Bryan",
                    LastName = "Prendergast", PersonCode = "EMP", PersonId = 1, Salary = 2000.00M }
                , new Person() { Honorific = "Mr.", FirstName = "Kevin", MiddleName = "Robert",
                    LastName = "Prendergast", PersonCode = "EMP", PersonId = 3, Salary = 2000.00M }
                , new Person() { Honorific = "Mrs.", FirstName = "Anne",
                    LastName = "Prendergast", PersonCode = "DEP", PersonId = 7, Salary = 2000.00M }
            };

            Person p = stringList.Where(s=> { return (s.PersonId == personId); }).FirstOrDefault();

            List<DeductionType> listOfDeductionTypes = new List<DeductionType>
            {
                (new DeductionType { AppliesToDep = true, DeductionTypeCode = "BEN", DepAmt = 19.00M })
            };

            IDeductionCalc _deductionCalcService;
            decimal? grossDeduction;

            _deductionCalcService = new DeductionCalcService();
            grossDeduction = _deductionCalcService.CalcDeduction(p, listOfDeductionTypes);

            Assert.AreEqual(grossDeduction, 19.00M);
        }
    }//end of class
}//end of namespace

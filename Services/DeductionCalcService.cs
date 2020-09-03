using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AugustCodingExercise
{
    public class DeductionCalcService : IDeductionCalc
    {
        private readonly CalcDBContext _calcContext;

        public DeductionCalcService(CalcDBContext calcContext)
        {
            _calcContext = calcContext;
        }

        public decimal? RetrieveDeduction(Person p)
        {
            DeductionType dt = null;
            decimal? deductionAmt = 0.00M;
            
            //TODO: consider refactoring to the Strategy design pattern here. Make each a class.
            if (p.PersonCode == "EMP")
            {
                dt = _calcContext.DeductionType.Find("BEN");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.EmpAmt;
                }
            }

            if (p.PersonCode == "DEP")
            {
                dt = _calcContext.DeductionType.Find("BEN");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.DepAmt;
                }
            }

            if (p.PersonCode == "SPO")
            {
                dt = _calcContext.DeductionType.Find("BEN");
                if (dt.AppliesToEmp == true)
                {
                    deductionAmt = dt.SpoAmt;
                }
            }

            return deductionAmt; 
        }

        public decimal? CalcDeduction(Person p)
        {
            return p.Salary - RetrieveDeduction(p); 
        }
    }
}

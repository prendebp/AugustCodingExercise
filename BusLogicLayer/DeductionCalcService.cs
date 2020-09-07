using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusLogicLayer
{
    public class DeductionCalcService : IDeductionCalc
    {
        private List<DeductionType> _listDedTypes;
        private DeductionType benefitsDeduction;

        public DeductionCalcService()
        {
        }

        public decimal? RetrieveDeduction(Person p)
        {
            decimal? deductionAmt = 0.00M;

            //TODO: consider refactoring to the Strategy design pattern here. Make each a class.
            if (p.PersonCode == "EMP")
            {
                if (benefitsDeduction.AppliesToEmp == true)
                {
                    deductionAmt = benefitsDeduction.EmpAmt;
                }
            }

            if (p.PersonCode == "DEP")
            {
                if (benefitsDeduction.AppliesToDep == true)
                {
                    deductionAmt = benefitsDeduction.DepAmt;
                }
            }

            if (p.PersonCode == "SPO")
            {
                if (benefitsDeduction.AppliesToSpo == true)
                {
                    deductionAmt = benefitsDeduction.SpoAmt;
                }
            }

            return deductionAmt;
        }

        public decimal? CalcDeduction(Person p, List<DeductionType> listDedTypes)
        {
            _listDedTypes = listDedTypes;
            benefitsDeduction = _listDedTypes.Where<DeductionType>(d => d.DeductionTypeCode == "BEN").FirstOrDefault<DeductionType>();

            return RetrieveDeduction(p);
        }
    }
}

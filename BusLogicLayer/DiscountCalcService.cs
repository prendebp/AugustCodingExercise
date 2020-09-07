using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusLogicLayer
{
    public class DiscountCalcService : IDiscountCalc
    {
        private List<DiscountType> _listOfDiscountTypes;
        private DiscountType discountTypeNameStartsWithA;

        public DiscountCalcService()
        {
        }

        private decimal? retrieveDiscountRate(Person p, List<DiscountType> listOfDiscountTypes)
        {
            decimal? discountRate = 0.00M;

            //TODO: consider refactoring to the Strategy design pattern here. Make each a class.
            if (p.PersonCode == "EMP")
            {
                if (discountTypeNameStartsWithA.AppliesToEmp == true && additionalDiscountConditionsApply(p))
                {
                    discountRate = discountTypeNameStartsWithA.EmpPercent;
                }
            }

            if (p.PersonCode == "DEP")
            {
                if (discountTypeNameStartsWithA.AppliesToDep == true && additionalDiscountConditionsApply(p))
                {
                    discountRate = discountTypeNameStartsWithA.DepPercent;
                }
            }

            if (p.PersonCode == "SPO")
            {
                if (discountTypeNameStartsWithA.AppliesToSpo == true && additionalDiscountConditionsApply(p))
                {
                    discountRate = discountTypeNameStartsWithA.SpoPercent;
                }
            }

            return discountRate;
        }

        private bool additionalDiscountConditionsApply(Person p)
        {
            return (p.FirstName.Split()[0].ToUpper().ToCharArray()[0]) == 'A';
        }

        public decimal? CalcDiscount(Person p, decimal? grossDeduction, List<DiscountType> listOfDiscountTypes)
        {
            _listOfDiscountTypes = listOfDiscountTypes;
            discountTypeNameStartsWithA = _listOfDiscountTypes.Where<DiscountType>(d => d.DiscountTypeCode == "NAM").FirstOrDefault<DiscountType>();

            return grossDeduction * retrieveDiscountRate(p, listOfDiscountTypes);
        }
    }
}

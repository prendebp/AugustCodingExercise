using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusLogicLayer
{
    public interface IDiscountCalc
    {
        decimal? CalcDiscount(Person p, decimal? grossDeduction, List<DiscountType> listOfDiscountTypes);
    }
}

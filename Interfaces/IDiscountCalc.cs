using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AugustCodingExercise
{
    public interface IDiscountCalc
    {
        decimal? CalcDiscount(Person p, decimal? grossDeduction);
    }
}

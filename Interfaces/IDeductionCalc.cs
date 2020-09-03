using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AugustCodingExercise
{
    public interface IDeductionCalc
    {
        decimal? CalcDeduction(Person p);
    }
}

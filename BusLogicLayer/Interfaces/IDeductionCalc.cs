using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusLogicLayer
{
    public interface IDeductionCalc
    {
        decimal? CalcDeduction(Person p, List<DeductionType> listDedTypes);
    }
}

using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AugustCodingExercise.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> FindPerson(long personId);
    }
}

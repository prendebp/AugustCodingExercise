
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AugustCodingExercise.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace AugustCodingExercise.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PeopleDBContext _peopleContext;

        public PersonRepository(PeopleDBContext peopleContext)
        {
            _peopleContext = peopleContext;
        }

        public async Task<Person> FindPerson(long personId)
        {
            return await _peopleContext.Person.FindAsync(personId);
        }
    }
}

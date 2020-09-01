
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AugustCodingExercise.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AugustCodingExercise.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AugustCodingDBContext _context;

        public PersonRepository(AugustCodingDBContext context)
        {
            _context = context;            
        }

        public Task<List<Person>> GetPeople()
        {
            return _context.Person.ToListAsync();
        }
    }
}

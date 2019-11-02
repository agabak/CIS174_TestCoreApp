using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CIS174_TestCoreApp.Services
{
    public class AccomplishmentService : IAccomplishmentService
    {
        private readonly DataContext _context;

        public AccomplishmentService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<PersonAccomplishmentViewModel> GetAccomplishments()
        {
            return _context.People.Include(x => x.Accomplishments)
                                  .Where(x => !x.IsDeleted)
                                  .Select(x => new PersonAccomplishmentViewModel
                                  {
                                      PersonId = x.PersonId,
                                      FirstName = x.FirstName,
                                      LastName = x.LastName,
                                      City = x.City,
                                      State = x.State,
                                      AccomplishmentCount = x.Accomplishments.Count()
                                  }).OrderByDescending(x => x.AccomplishmentCount)
                                  .ToList();
        }

        public PersonDetailAccomplishmentViewModel GetAccomplisment(int id)
        {
            return _context.People.Include(x => x.Accomplishments)
                                   .Select(x => new PersonDetailAccomplishmentViewModel
                                    {
                                        PersonId = x.PersonId,
                                        FirstName = x.FirstName,
                                        LastName = x.LastName,
                                        City = x.City,
                                        State = x.State,
                                        Accomplishments = x.Accomplishments.Select(y => new AccomplishmentViewModel
                                        {
                                            Name = y.Name,
                                            DateOfAccomplishment = y.DateOfAccomplishment.ToString("MM/dd/yyyy")
                                        })
                                    })
                                      .FirstOrDefault(x => x.PersonId == id);
        }

        public bool Delete(int id)
        {
           var person =  _context.People.Find(id);
            if(person != null)
            {
                person.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

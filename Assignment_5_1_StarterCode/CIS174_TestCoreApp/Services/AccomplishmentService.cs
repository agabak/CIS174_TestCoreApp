using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
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
            return _context.People
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
                                            Id = y.AccomplishmentId,
                                            Name = y.Name,
                                            DateOfAccomplishment = y.DateOfAccomplishment.ToString("MM/dd/yyyy"),
                                            PersonId = y.PersonId
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

        public PersonDetailAccomplishmentViewModel Edit(PersonDetailAccomplishmentViewModel model)
        {
            var person = _context.People.Include(x => x.Accomplishments).FirstOrDefault(p => p.PersonId == model.PersonId);
            if (person != null)
            {
                person.FirstName = model.FirstName;
                person.LastName = model.LastName;
                person.City = model.City;
                person.State = model.State;
                model.Accomplishments = person.Accomplishments.Select(x => new AccomplishmentViewModel
                                                {
                                                    Id = x.AccomplishmentId,
                                                    Name = x.Name,
                                                    DateOfAccomplishment = x.DateOfAccomplishment.ToString("MM/dd/yyyy")
                                                 });
                _context.SaveChanges();
            }

            return model;
        }

        public AccomplishmentViewModel GetSingleAccomplishment(int id)
        {
            return _context.Accomplishments.Select(x => new AccomplishmentViewModel
                            {
                                Id = x.AccomplishmentId,
                                Name = x.Name,
                                DateOfAccomplishment = x.DateOfAccomplishment.ToString("MM/dd/yyyy")
                            }).FirstOrDefault(x => x.Id == id);
        }

        public AccomplishmentViewModel UpdateAccomplishmnet(AccomplishmentViewModel model)
        {
            var accomplishment = _context.Accomplishments.FirstOrDefault(X => X.AccomplishmentId == model.Id);
            if(accomplishment != null)
            {
                accomplishment.Name = model.Name;
                accomplishment.DateOfAccomplishment = Convert.ToDateTime(model.DateOfAccomplishment);
                _context.SaveChanges();
            }

            return model;
        }

        public bool Create(PersonAccomplishmentViewModel model)
        {
            var person = new Person
                       {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Birthdate = model.DateOfBirth,
                            City = model.City,
                            State = model.State
                         };
            _context.People.Add(person);

            if (_context.SaveChanges() > -1) return true;
            return false;
        }

        public bool CreateAccomplishment(CreateAccomplishmentViewModel model)
        {
            var accomplishment = new Accomplishment
                        {
                            Name = model.Name,
                            DateOfAccomplishment = model.DateOfAccomplishment,
                            PersonId = model.PersonId
                        };
            _context.Accomplishments.Add(accomplishment);

            if (_context.SaveChanges() > -1) return true;

            return false;
        }
    }
}  

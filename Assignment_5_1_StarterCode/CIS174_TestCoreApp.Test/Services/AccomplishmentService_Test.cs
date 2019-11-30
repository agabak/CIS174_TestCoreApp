using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace CIS174_TestCoreApp.Test.Services
{

    public class AccomplishmentService_Test
    {

        [Fact]
        public void AccomplishmentService_Return_AccomplishmentList()
        {

            var options = FakeDataDBContext();
            using (var context = new DataContext(options))
            {
                var service = new AccomplishmentService(context);
                var results = service.GetAccomplishments();

                var count = results.Count();
                //Assert
                Assert.Equal(3, count);
            } 
        }

        [Fact]
        public void AccomplishmentService_Return_Accomplishment()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                               .UseInMemoryDatabase(databaseName: "recipedb")
                               .Options;

            using (var context = new DataContext(options))
            {
                var service = new AccomplishmentService(context);
                var result = service.GetAccomplisment(2);

                //Assert
                Assert.Equal("FirstName2", result.FirstName);
            }
        }

        [Fact]
        public void AccomplishmentService_Return_Accomplishment_Delete()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                               .UseInMemoryDatabase(databaseName: "recipedb")
                               .Options;

            using (var context = new DataContext(options))
            {
                var service = new AccomplishmentService(context);
                var result = service.Delete(3);

                //Assert
                Assert.True(result);
            }
        }

        private DbContextOptions<DataContext> FakeDataDBContext()
        {

            var options = new DbContextOptionsBuilder<DataContext>()
                              .UseInMemoryDatabase(databaseName: "recipedb")
                              .Options;

            using (var context = new DataContext(options))
            {
                context.People.Add(new Person
                {
                    PersonId = 1,
                    FirstName = "FirstName1",
                    LastName = "LasName1",
                    Birthdate = new DateTime(1976, 11, 20),
                    City = "City1",
                    State = "",
                    IsDeleted = false,
                    Accomplishments = new List<Accomplishment>
                             { new Accomplishment { AccomplishmentId = 1, PersonId = 1, Name = "Name1", DateOfAccomplishment = new DateTime(2019,11,17) } }
                });
                context.People.Add(new Person
                {
                    PersonId = 2,
                    FirstName = "FirstName2",
                    LastName = "LasName2",
                    Birthdate = new DateTime(1976, 11, 20),
                    City = "City2",
                    State = "",
                    IsDeleted = false,
                    Accomplishments = new List<Accomplishment>
                             { new Accomplishment { Name = "Name2", DateOfAccomplishment = new DateTime(2019,11,17) } }
                });
                context.People.Add(new Person
                {
                    PersonId = 3,
                    FirstName = "FirstName3",
                    LastName = "LasName3",
                    Birthdate = new DateTime(1976, 11, 20),
                    City = "City3",
                    State = "",
                    IsDeleted = false,
                    Accomplishments = new List<Accomplishment>
                             { new Accomplishment { Name = "Name3", DateOfAccomplishment = new DateTime(2019,11,17) } }
                });
                context.SaveChanges();
            }

            return options;

        }
    }
}

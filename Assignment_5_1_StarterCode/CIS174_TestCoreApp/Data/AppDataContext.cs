using CIS174_TestCoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIS174_TestCoreApp.Data
{
    public class AppDataContext: DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Accomplishment> Accomplishments { get; set; }
    }
}

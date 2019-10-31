using CIS174_TestCoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CIS174_TestCoreApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }       
}
        
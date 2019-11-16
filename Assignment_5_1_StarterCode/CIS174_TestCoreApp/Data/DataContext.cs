using CIS174_TestCoreApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CIS174_TestCoreApp.Data
{
    public class DataContext: IdentityUserContext<UserPerson>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Accomplishment> Accomplishments { get; set; }
        public DbSet<LogsRequestAndResponse> logsRequestAndResponses { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<UserPerson> UserPeople { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<UserPerson>().Property<bool>(p => p.IsActive).HasDefaultValue<bool>(true);
            builder.Entity<IdentityUserLogin<Guid>>().HasKey(p => p.UserId);
           
            base.OnModelCreating(builder);
        }
    }       
}
        
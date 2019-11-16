using Microsoft.AspNetCore.Identity;
using System;

namespace CIS174_TestCoreApp.Entities
{
    public class UserPerson : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

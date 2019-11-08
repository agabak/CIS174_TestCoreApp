﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Entities
{
    public class UserPerson : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class PersonAccomplishmentViewModel
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  City { get; set; }
        public string State { get; set; }
        public int AccomplishmentCount { get; set; }
    }
}

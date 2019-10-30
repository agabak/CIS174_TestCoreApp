using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public virtual ICollection<Accomplishment> Accomplishments { get; set; }

    }
}



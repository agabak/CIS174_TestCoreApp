using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class PersonAccomplishmentViewModel
    {
        public int PersonId { get; set; }
        [Required]
        [Display(Name ="First name")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        public string  City { get; set; }
        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name ="DOB")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int AccomplishmentCount { get; set; }
    }
}

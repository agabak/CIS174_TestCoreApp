using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class PersonDetailAccomplishmentViewModel
    {
        public int PersonId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        public IEnumerable<AccomplishmentViewModel> Accomplishments { get; set; }

    }
}

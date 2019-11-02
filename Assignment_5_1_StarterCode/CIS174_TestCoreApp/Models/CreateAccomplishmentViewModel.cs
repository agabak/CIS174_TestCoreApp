using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class CreateAccomplishmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Date of Accomplishment")]
        [DataType(DataType.Date)]
        public DateTime DateOfAccomplishment { get; set; }
        public int PersonId { get; set; }
    }
}

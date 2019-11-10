using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class UserManagerUpdateCommandModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name ="First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string EmailAddress { get; set; }
        [Display(Name ="Username")]
        public string UserName { get; set; }
        [Required]
        [Phone]
        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }
    }
}

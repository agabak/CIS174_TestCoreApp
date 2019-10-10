using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIS174_TestCoreApp.Models
{
    public class CreatePersonViewModel
    {
        [Required]
        [StringLength(25)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
       
        [Required]
        [StringLength(25, MinimumLength = 2)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Age { get; set; }

        [Display(Name ="Date Of Birth")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2018",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Country { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; } = new List<SelectListItem>
                    {
                       new SelectListItem{Value =  "USA", Text ="USA" },
                       new SelectListItem{Value ="Mexico", Text ="Mexico"},
                       new SelectListItem {Value ="Canada", Text ="Canada"}
                    };
    }
}

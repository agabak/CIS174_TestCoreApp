﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class RegisterCommandModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Compare("Email")]
        public string EmailConfirm { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}

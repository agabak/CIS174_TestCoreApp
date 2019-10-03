using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIS174_TestCoreApp.Models;

namespace CIS174_TestCoreApp.Controllers
{
   
    public class StudentController: Controller
    {
        public IActionResult Index([FromRoute]int id)
        {
            var studentsList = new List<StudentViewModel>
            {
                new StudentViewModel {FirstName ="John", LastName = "Doo", Grade = 54},
                new StudentViewModel {FirstName ="Juma", LastName = "Ally", Grade = 67},
                new StudentViewModel {FirstName ="Hammis", LastName = "Jullies", Grade = 70},
                new StudentViewModel {FirstName ="Mike", LastName = "Hilty", Grade = 80}
            };

            var viewModel = new ViewModel
                            {
                                AccessLevel = id,
                                Students = studentsList
                            };
            
            return View(viewModel);
        }
    }
}

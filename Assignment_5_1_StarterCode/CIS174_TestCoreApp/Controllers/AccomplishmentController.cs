using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    public class AccomplishmentController: Controller
    {

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public  IActionResult Create()
        {
            return View();
        }
    }
}

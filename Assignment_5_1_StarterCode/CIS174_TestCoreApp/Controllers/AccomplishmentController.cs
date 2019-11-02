using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    public class AccomplishmentController : Controller
    {
        private readonly IAccomplishmentService _accomplishment;

        public AccomplishmentController(IAccomplishmentService accomplishment)
        {
            _accomplishment = accomplishment;
        }

        public IActionResult List()
        {
            return View(_accomplishment.GetAccomplishments());
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            return View(_accomplishment.GetAccomplisment(id));
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
           if( _accomplishment.Delete(id)) return RedirectToAction("List");

            return View(_accomplishment.GetAccomplisment(id));
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View(_accomplishment.GetAccomplisment(id));
        }


        public  IActionResult Create()
        {
            return View();
        }
    }
}

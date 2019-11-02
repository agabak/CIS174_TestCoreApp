using CIS174_TestCoreApp.Models;
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

        [HttpPost("edit/{id}")]
        public IActionResult Edit([FromForm] PersonDetailAccomplishmentViewModel model, int id)
        {
            if (ModelState.IsValid && model.PersonId == id)
            {
                model = _accomplishment.Edit(model);
            }
            return View(model);
        }

        [HttpGet("accomplishmentEdit/{id}")]
        public IActionResult AccomplishmentEdit(int id)
        { 
            return View(_accomplishment.GetSingleAccomplishment(id));
        }

        [HttpPost("accomplishmentEdit/{id}")]
        public IActionResult AccomplishmentEdit([FromForm]AccomplishmentViewModel model,int id)
        {
            if(model.Id == id && ModelState.IsValid)
            {
                model = _accomplishment.UpdateAccomplishmnet(model);
            }
            return RedirectToAction("List");
        }

        public  IActionResult Create()
        {
            return View();
        }
    }
}

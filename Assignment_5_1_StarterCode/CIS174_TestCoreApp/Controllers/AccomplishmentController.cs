using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    public class AccomplishmentController : Controller
    {
        private readonly IAccomplishmentService _accomplishment;
        private readonly IAuthorizationService _service;

        public AccomplishmentController(IAccomplishmentService accomplishment, IAuthorizationService service)
        {
            _accomplishment = accomplishment;
            _service = service;
        }

        [Authorize("IsAdmin")]
        public IActionResult List()
        {
            var user = this.User.HasClaim(c => c.Type == "Admin") || this.User.HasClaim(c => c.Type == "Email");

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

        [Authorize]
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View(_accomplishment.GetAccomplisment(id));
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] PersonDetailAccomplishmentViewModel model, int id)
        {
            if (ModelState.IsValid && model.PersonId == id)
            {
                model = _accomplishment.Edit(model);

                var person = new Person
                            {
                                LastName = model.LastName,
                                State = model.State
                            };

                var authResult = await _service.AuthorizeAsync(User, person, "CanEditContent");
                if(!authResult.Succeeded)
                {
                    return new  ForbidResult();
                }
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

        [HttpPost]
        public IActionResult Create([FromForm] PersonAccomplishmentViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (_accomplishment.Create(model)) return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Fail to create user");
            return View(model);
        }

        [HttpGet("createAccomplishment/{id}")]
        public IActionResult CreateAccomplishment(int id)
        {
            var model = new CreateAccomplishmentViewModel { PersonId = id };
            return View(model);
        }

        [HttpPost("createAccomplishment/{id}")]
        public IActionResult CreateAccomplishment([FromForm]CreateAccomplishmentViewModel model, int id)
        {
            if(ModelState.IsValid)
            {
                model.PersonId = id;
                if (_accomplishment.CreateAccomplishment(model)) return RedirectToAction("List");
            }
            return View(model);
        }

    }
}

using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private readonly IPersonManagerService _service;

        public ManagerController(IPersonManagerService service)
        {
            _service = service;     
        }

        public async Task<IActionResult> Update()
        {
            var manageUser = await _service.FindUserByName(User.Identity.Name);
            if (manageUser == null)
            {
                ModelState.TryAddModelError("", "Unable to get the user");
                return View();
            }
            return View(manageUser);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserManagerUpdateCommandModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _service.UpdateUser(model)) return RedirectToAction("Index", "Home");

            ModelState.TryAddModelError("", "Somthing went wrong unable to update"); 
            return View(model);
        }
    }
}

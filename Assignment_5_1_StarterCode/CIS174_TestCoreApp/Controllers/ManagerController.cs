using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
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
        private readonly UserManager<UserPerson> _userManager;

        public ManagerController(UserManager<UserPerson> userManager)
        {
            _userManager = userManager;     
        }

        public async Task<IActionResult> Update()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                ModelState.AddModelError("", "something went wrong");
                return View();
            }

            var manageUser = new UserManagerUpdateCommandModel
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                EmailAddress = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                UserName = user.UserName,
                            };

            return View(manageUser);
        }
    }
}

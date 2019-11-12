using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<UserPerson> _userManager;
        private readonly SignInManager<UserPerson> _signInManager;
        private readonly IPersonManagerService _service;

        public AccountController(SignInManager<UserPerson> signInManager, 
                                 UserManager<UserPerson> userManager, IPersonManagerService service)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _service = service;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommandModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var isRegostered = await _service.Register(model);
         
            if (isRegostered) return RedirectToAction("Index", "Home");
           
                
            ModelState.AddModelError("", "Enable to register a user");
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _userManager.FindByNameAsync(model?.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "User doesn't exist");
                return View(model);
            }

            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

            if (signIn.Succeeded)
            {
                if (Request.Query.ContainsKey("ReturnUrl")) return Redirect(Request.Query.Keys.FirstOrDefault());

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong, you can't login");

            return View(model);
        }

        public IActionResult Logout()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }



    }
}

using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
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

        public AccountController(SignInManager<UserPerson> signInManager, 
                                 UserManager<UserPerson> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

               var storeUser = new UserPerson
                                {
                                    FirstName = model?.FirstName,
                                    LastName = model?.LastName,
                                    Email = model?.Email,
                                    UserName = model?.Username,
                                    PhoneNumber = model?.PhoneNumber
                                };

             var isCreated = await _userManager.CreateAsync(storeUser, model.Password);

            if (isCreated.Succeeded)
            {
                await _signInManager.SignInAsync(storeUser, false, null);
                return RedirectToAction("Home", "Index");

            }
                
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

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("", "enable to login");
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

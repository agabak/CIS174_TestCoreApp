using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers
{
    public class AccountController: Controller
    { 
        private readonly IPersonManagerService _service;

        public AccountController(IPersonManagerService service)
        {
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

            var isLogin = await _service.Login(model);
            if (isLogin)
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
                _service.Logout();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

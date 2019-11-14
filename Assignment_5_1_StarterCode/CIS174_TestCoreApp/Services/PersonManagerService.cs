using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CIS174_TestCoreApp.Services
{
    public class PersonManagerService : IPersonManagerService
    {
        private readonly UserManager<UserPerson> _userManager;
        private readonly DataContext _context;
        private readonly SignInManager<UserPerson> _signInManager;

        public PersonManagerService(UserManager<UserPerson> userManager,
                                   SignInManager<UserPerson> signInManager,
                                   DataContext context)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;

        }


        public async Task<UserManagerUpdateCommandModel> FindUserByName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return null;

            var manageUser = new UserManagerUpdateCommandModel
                                {
                                    FirstName = user.FirstName,
                                    LastName = user.LastName,
                                    EmailAddress = user.Email,
                                    PhoneNumber = user.PhoneNumber,
                                    UserName = user.UserName,
                                };

            return manageUser;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model?.Username);

            if (user == null) return false;
  
            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

            if (signIn.Succeeded) return true;

            return false;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public async Task<bool> Register(RegisterCommandModel model)
        {
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
                if(model.Email == "valatorre@dmacc.edu" || model.Email == "adminagaba@gmail.com")
                {
                    var claims = new List<Claim>
                    {
                        new Claim("FullName", model.FirstName + ", " + model.LastName),
                        new Claim("Admin", model.Email)
                    };
                    await _userManager.AddClaimsAsync(storeUser, claims);
                }else
                {
                    var claim = new Claim("FullName", model.FirstName + ", " + model.LastName);
                    await _userManager.AddClaimAsync(storeUser, claim);
                }
                
             
                await _signInManager.SignInAsync(storeUser, false, null);

                return true;
            }

            return false;
       }

        public async Task<bool> UpdateUser(UserManagerUpdateCommandModel model)
        {
             var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null) return false;
            
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.EmailAddress;
                user.PhoneNumber = model.PhoneNumber;
                user.UserName = model.UserName;
        
        var isUpdated =  await  _context.SaveChangesAsync();
            if (isUpdated > -1) return true;

            return false;

        }
    }
}

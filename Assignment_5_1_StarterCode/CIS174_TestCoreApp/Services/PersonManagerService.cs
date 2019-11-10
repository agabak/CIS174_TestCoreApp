using System;
using System.Collections.Generic;
using System.Linq;
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

        public PersonManagerService(UserManager<UserPerson> userManager,
                                                    DataContext context)
        {
            _userManager = userManager;
            _context = context;

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

using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Filters
{
    public class IsActiveUserHandler : AuthorizationHandler<IsActiveUser>
    {
        private readonly UserManager<UserPerson> _userManager;

        public IsActiveUserHandler(UserManager<UserPerson> userManager)
        {
            _userManager = userManager;
        }

        protected  override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsActiveUser requirement)
        {
           if(context.User.Identity.IsAuthenticated)
            {
                var user =  _userManager.FindByNameAsync(context.User.Identity.Name).Result;
                if (user == null) context.Fail();

                if (!user.IsActive) context.Fail();

                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

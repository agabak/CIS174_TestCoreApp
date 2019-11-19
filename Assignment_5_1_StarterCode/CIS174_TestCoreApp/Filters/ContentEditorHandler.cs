using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Filters
{
    public class ContentEditorHandler : AuthorizationHandler<ContentEditorRequirement, Person>
    {
        private readonly UserManager<UserPerson> _service;
        private readonly DataContext _context;

        public ContentEditorHandler(UserManager<UserPerson> service, DataContext context)
        {
            _service = service;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ContentEditorRequirement requirement, Person resource)
        {
            if(context.User.HasClaim(c => c.Type == "Admin"))
            {  // This for admin user
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var appUser = _service.FindByNameAsync(context.User.Identity.Name).Result;

            if (resource.LastName.ToUpper().Equals(appUser.LastName.ToUpper()) &&
                resource.FirstName.ToUpper().Equals(appUser.FirstName
                .ToUpper()))
            {
                context.Succeed(requirement);
            }

            if(resource.State != null && appUser.State != null &&
                resource.State.ToUpper().Equals(appUser?.State.ToUpper())) context.Succeed(requirement);

            if(context.User.HasClaim(X => X.Type == "ContentEditor")) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

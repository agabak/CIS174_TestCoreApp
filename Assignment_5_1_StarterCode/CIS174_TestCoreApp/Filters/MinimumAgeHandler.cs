using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Filters
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            throw new Exception("Your sick individual");
            if (!context.User.HasClaim(c => c.Type == "DateOfBirth"))
            {
                return Task.CompletedTask;
            }

            var valueDate = context.User.FindFirst(c => c.Type == "DateOfBirth").Value.Trim();
          
            var dateOfBirth = Convert.ToDateTime(valueDate);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }
            
            if (calculatedAge >= requirement.MinimunAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Text;

namespace CIS174_TestCoreApp.Filters
{
    public class ValidatePersonInfoFilter : IActionFilter
    {

        private readonly IAccomplishmentService _service;
        public ValidatePersonInfoFilter(IAccomplishmentService  service)
        {
            _service = service;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var model = context.HttpContext.Request.Body.GetType();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = context.ActionArguments["model"];
        }
    }


    public class ValidatePersonInfoFilterAttribute : TypeFilterAttribute
    {
        public ValidatePersonInfoFilterAttribute(): base(typeof(ValidatePersonInfoFilter))
        {

        }

    }
}

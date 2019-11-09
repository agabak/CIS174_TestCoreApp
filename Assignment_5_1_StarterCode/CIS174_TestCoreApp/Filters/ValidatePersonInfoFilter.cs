﻿using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {   
            // convert action argument to  view model
            var model = (PersonDetailAccomplishmentViewModel)context.ActionArguments["model"];


            var person = _service.GetAccomplisment(model.PersonId);

            if(person == null)
            {
                context.Result = new NotFoundObjectResult(context.ModelState);
            }

            if(person.FirstName != person.FirstName)
            {
                // i return bad request 
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }
    }


    public class ValidatePersonInfoFilterAttribute : TypeFilterAttribute
    {
        public ValidatePersonInfoFilterAttribute(): base(typeof(ValidatePersonInfoFilter))
        {

        }

    }
}

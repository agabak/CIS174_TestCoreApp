using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CIS174_TestCoreApp.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly DataContext _context;
        
        public CustomExceptionFilter(DataContext context)
        {
            _context = context;
        }

       public void OnException(ExceptionContext context)
       {
            
            Guid.TryParse(context.HttpContext.Items["requestId"].ToString(), out var requestId);

            var responseError = new ErrorLog
                        {
                            HttpStatusCode = context.HttpContext.Response.StatusCode,
                            RequestedId = requestId,
                            Time = DateTime.Now,
                            ErrorMessage = context.Exception.Message,
                            StackTrace = context.Exception.StackTrace
                        };

            context.Result = new ObjectResult(responseError)
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;

            _context.ErrorLogs.Add(responseError);
            _context.SaveChanges();
        }
    }

    public class CustomExceptionFilterAttribute : TypeFilterAttribute
    {
        public CustomExceptionFilterAttribute() : base(typeof(CustomExceptionFilter))
        {
        }
    }
}

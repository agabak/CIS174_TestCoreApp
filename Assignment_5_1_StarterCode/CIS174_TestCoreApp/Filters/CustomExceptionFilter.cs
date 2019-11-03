using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            
            var responseError = new ErrorLog
                                {
                                    HttpStatusCode = context.HttpContext.Response.StatusCode,
                                    RequestedId = Guid.Parse(context.HttpContext.Items["reqestedId"].ToString()),
                                    Time = DateTime.Now,
                                    ErrorMessage = context.Exception.Message,
                                    StackTrace = context.Exception.StackTrace
                                };
            var test = responseError;

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

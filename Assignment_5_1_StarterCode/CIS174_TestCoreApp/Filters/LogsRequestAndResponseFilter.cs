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
    public class LogsRequestAndResponseFilter :  IResourceFilter
    {
        private readonly DataContext _context;

        public LogsRequestAndResponseFilter(DataContext context)
        {
            _context = context;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var result = context.HttpContext;
            var requestId = Guid.NewGuid();
                result.Items.Add("requestId", requestId);
            // Insert the log in the database
            _context.logsRequestAndResponses.Add(new LogsRequestAndResponse { RequestId = requestId, HttpContext = result.ToString() });
            _context.SaveChanges();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class LogsRequestAndResponseFilterAttribute : TypeFilterAttribute
    {
        public LogsRequestAndResponseFilterAttribute() : base(typeof(LogsRequestAndResponseFilter))
        {}
    }
}

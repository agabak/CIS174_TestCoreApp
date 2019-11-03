using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;

namespace CIS174_TestCoreApp.Filters
{
    public class LogsRequestAndResponseFilter :  IResourceFilter
    {
        private readonly DataContext _context;
      
        private  bool IsEnabled { get; set; }

        public LogsRequestAndResponseFilter(DataContext context, IConfiguration configuration)
        {
            _context = context;
            IsEnabled = configuration.GetValue<bool>("IsEnable");
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!IsEnabled) return;
            var result = context.HttpContext;
            var requestId = Guid.NewGuid();
                result.Items.Add("requestId", requestId);
            // Insert the log in the database
            _context.logsRequestAndResponses.Add(new LogsRequestAndResponse { RequestId = requestId, HttpContext = result.ToString() });
            _context.SaveChanges();
        }

        public void OnResourceExecuted(ResourceExecutedContext context){}
    }

    public class LogsRequestAndResponseFilterAttribute : TypeFilterAttribute
    {
        public LogsRequestAndResponseFilterAttribute() : base(typeof(LogsRequestAndResponseFilter))
        {}
    }
}

using System;

namespace CIS174_TestCoreApp.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public int HttpStatusCode { get; set; }
        public Guid RequestedId { get; set; }
        public DateTime Time { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}

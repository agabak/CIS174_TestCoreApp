using System;

namespace CIS174_TestCoreApp.Entities
{
    public class Accomplishment
    {
        public int AccomplishmentId { get; set; }
        public int  PersonId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfAccomplishment { get; set; }
        public virtual Person Person { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class ViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }
        public int AccessLevel { get; set; }
    }
}

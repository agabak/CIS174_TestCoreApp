using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class RecipeSummaryViewModel
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public string TimeToCook { get; internal set; }
    }
}

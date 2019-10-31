using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class UpdateRecipeCommand
    {
        public object[] Id { get; internal set; }
        public string Name { get; internal set; }
        public int TimeToCookHrs { get; internal set; }
        public int TimeToCookMins { get; internal set; }
        public bool IsVegetarian { get; internal set; }
        public bool IsVegan { get; internal set; }
    }
}

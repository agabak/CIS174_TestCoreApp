using CIS174_TestCoreApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class RecipeViewModel
    {
        public RecipeViewModel()
        {
            Ingredients = new List<Ingredient>();   
        }
        public string Name { get; internal set; }
        public int TimeToCookHrs { get; internal set; }
        public int TimeToCookMin { get; internal set; }
        public string Method { get; internal set; }
        public bool IsVegetarian { get; internal set; }
        public bool IsVegan { get; internal set; }
        public List<Ingredient> Ingredients { get; internal set; }
    }
}

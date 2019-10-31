using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Models
{
    public class RecipeDetailViewModel
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public object Ingredients { get; internal set; }
        public string Method { get; internal set; }

        public class Item
        {
            public string Name { get; set; }
            public string Quantity { get; set; }
        }
    }
}

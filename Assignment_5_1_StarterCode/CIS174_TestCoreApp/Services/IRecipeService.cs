using CIS174_TestCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Services
{
    public interface IRecipeService
    {
        int CreateRecipe(RecipeViewModel cmd);
        ICollection<RecipeSummaryViewModel> GetRecipes();
        RecipeDetailViewModel GetRecipeDetail(int id);
        UpdateRecipeCommand UpdateRecipe(UpdateRecipeCommand cmd);
        bool DeleteRecipe(int id);

    }
}


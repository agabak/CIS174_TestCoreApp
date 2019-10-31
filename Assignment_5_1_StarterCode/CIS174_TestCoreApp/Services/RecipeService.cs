using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS174_TestCoreApp.Data;
using CIS174_TestCoreApp.Entities;
using CIS174_TestCoreApp.Models;

namespace CIS174_TestCoreApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly DataContext _context;

        public RecipeService(DataContext context)
        {
            _context = context;

        }

        public int CreateRecipe(RecipeViewModel cmd)
        {
            var recipe = new Recipe
            {
                Name = cmd.Name,
                TimeToCook = new TimeSpan(cmd.TimeToCookHrs, cmd.TimeToCookMin, 0), 
                Method = cmd.Method,
                IsVegetarian = cmd.IsVegetarian,
                IsVegan = cmd.IsVegan,
                Ingredients = cmd.Ingredients.Select(i => new Ingredient
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                }).ToList()
            };

            _context.Recipes.Add(recipe);
            _context.SaveChanges();
            return recipe.RecipeId;
        }

        public bool DeleteRecipe(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(X => X.RecipeId == id);
            if (recipe == null) return false;
            recipe.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public RecipeDetailViewModel GetRecipeDetail(int id)
        {
            return _context.Recipes.Where(x => x.RecipeId == id).Select(x => new RecipeDetailViewModel
            {
                Id = x.RecipeId,
                Name = x.Name,
                Method = x.Method,
                Ingredients = x.Ingredients
               .Select(item => new RecipeDetailViewModel.Item
               {
                   Name = item.Name,
                   Quantity = $"{item.Quantity} {item.Unit}"
               })
            }).SingleOrDefault();
        }

        public ICollection<RecipeSummaryViewModel> GetRecipes()
        {

            return _context.Recipes.Where(r => !r.IsDeleted)
                                     .Select(r => new RecipeSummaryViewModel
                                     {
                                         Id = r.RecipeId,
                                         Name = r.Name,
                                         TimeToCook = $"{r.TimeToCook.TotalMinutes}mins"
                                     }).ToList();
        }

        public UpdateRecipeCommand UpdateRecipe(UpdateRecipeCommand cmd)
        {
            var recipe = _context.Recipes.Find(cmd.Id);
            if(recipe == null)
            {
                throw new Exception("Unable to find the recipe");
            }
            UpdateRecipe(recipe, cmd);
            _context.SaveChanges();
            return cmd;
        }

        private void UpdateRecipe(Recipe recipe, UpdateRecipeCommand cmd)
        {
            recipe.Name = cmd.Name;
            recipe.TimeToCook = new TimeSpan(cmd.TimeToCookHrs, cmd.TimeToCookMins, 0);
            recipe.IsVegetarian = cmd.IsVegetarian;
            recipe.IsVegan = cmd.IsVegan;
        }
    }
}
 
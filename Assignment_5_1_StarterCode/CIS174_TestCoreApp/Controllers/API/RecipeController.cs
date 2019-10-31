using CIS174_TestCoreApp.Models;
using CIS174_TestCoreApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController: Controller
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpPost("{create}")]
        public IActionResult Create(RecipeViewModel model)
        {
            if(ModelState.IsValid)
            {
                _service.CreateRecipe(model);
                return Ok(model);
            }

            return BadRequest();
        }

        [HttpGet("{Recipes}")]
        public IActionResult Recipes()
        {
            var recipes = _service.GetRecipes();
            if (!recipes.Any()) return NotFound();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult Recipe(int id)
        {
            var recipe = _service.GetRecipeDetail(id);
            if (recipe == null) return NotFound();
            return Ok(recipe);
        }

        [HttpPut]
        public IActionResult Update(UpdateRecipeCommand cmd)
        {
            var recipe = _service.UpdateRecipe(cmd);
            return Ok(cmd);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _service.DeleteRecipe(id);
            if (!isDeleted) return BadRequest("Something went wrong");
            return NoContent();
        }
    }
}

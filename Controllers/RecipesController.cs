using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Data;
using MiRecetaSecretaAPI.Models;

namespace MiRecetaSecretaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("SecretRecipeFront")]
    public class RecipesController : Controller
    {
        private readonly AppDBContext _dbContext;
        public RecipesController(AppDBContext db) 
        { 
            _dbContext = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return Ok(await _dbContext.Recipe.Where(x => x.Status == 1).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _dbContext.Recipe.FirstOrDefaultAsync(x => x.Id == id && x.Status == 1);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(recipe.RecipeIngredients == null || recipe.RecipeTags == null)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Recipe.Add(recipe);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dbContext.Recipe.Update(recipe);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _dbContext.Recipe.FirstOrDefaultAsync(i => i.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.Status = 0;
            _dbContext.Recipe.Update(recipe); 

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }

        [HttpGet("by-query/{query}")]
        public async Task<IActionResult> GetMatchIngredients(string query)
        {
            List<string> ids = query.Split(',').ToList();

            var recipes = await _dbContext.Recipe.Where(x => x.Status == 1)
                                .Include(x => x.RecipeIngredients)
                                .ToListAsync();
            
            var bestMatch = recipes.Select(recipe => new
            {
                Recipe = recipe,
                Count = recipe.RecipeIngredients.Count(ri => ids.Contains(ri.IngredientId.ToString()))
            })
            .OrderByDescending(x => x.Count)
            .FirstOrDefault(x => x.Count > 0);

            if (bestMatch == null)
            {
                return NotFound("No matching recipes found.");
            }

            return Ok(bestMatch.Recipe);
        }
    }
}

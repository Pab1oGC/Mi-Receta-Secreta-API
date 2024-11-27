using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Data;
using MiRecetaSecretaAPI.Models;

namespace MiRecetaSecretaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly AppDBContext _db;
        public IngredientsController(AppDBContext appDBContext) 
        { 
            _db = appDBContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            return Ok(await _db.Ingredient.Where(x => x.Status == 1).ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var product = await _db.Ingredient.FirstOrDefaultAsync(x => x.Id == id && x.Status == 1);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Ingredient.Add(ingredient);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.Id }, ingredient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredientAllData = await _db.Ingredient
                                .Include(ri => ri.RecipeIngredients)
                                    .ThenInclude(r => r.Recipe)
                                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredientAllData == null)
            {
                return NotFound();
            }

            ingredientAllData.Status = 0;
            _db.Entry(ingredientAllData).Property(x => x.Status).IsModified = true;

            foreach (var recipe in ingredientAllData.RecipeIngredients)
            {
                recipe.Recipe.Status = 0;
                _db.Entry(recipe.Recipe).Property(x => x.Status).IsModified= true;
            }
            
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Ingredient.Update(ingredient);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }
    }
}

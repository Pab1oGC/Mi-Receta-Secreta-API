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
            return Ok(await _db.Ingredients.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var product = await _db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
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

            _db.Ingredients.Add(ingredient);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.Id }, ingredient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _db.Ingredients.FirstOrDefaultAsync(x => x.Id == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            ingredient.Status = 0;
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

            _db.Entry(ingredient).State = EntityState.Modified;

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

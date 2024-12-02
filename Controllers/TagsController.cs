using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Data;
using MiRecetaSecretaAPI.Models;

namespace MiRecetaSecretaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("SecretRecipeFront")]
    public class TagsController : Controller
    {
        private readonly AppDBContext _dbContext;
        public TagsController(AppDBContext db)
        {
            _dbContext = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return Ok(await _dbContext.Tag.Where(x => x.Status == 1).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _dbContext.Tag.FirstOrDefaultAsync(x => x.Id == id && x.Status == 1);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> PostIngredient(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Tag.Add(tag);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dbContext.Tag.Update(tag);

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

            var tagAllData = await _dbContext.Tag.Include(rt => rt.RecipeTags)!.ThenInclude(r => r.Recipe).FirstOrDefaultAsync(t => t.Id == id);

            if (tagAllData == null)
            {
                return NotFound();
            }

            tagAllData.Status = 0;
            _dbContext.Tag.Update(tagAllData);

            foreach (var recipeTag in tagAllData.RecipeTags!)
            {
                recipeTag.Recipe!.Status = 0;
                _dbContext.Recipe.Update(recipeTag.Recipe);
            }

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
    }
}

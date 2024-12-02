using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Models;
using System.Data;

namespace MiRecetaSecretaAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeTag> RecipeTag { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }



    }
}

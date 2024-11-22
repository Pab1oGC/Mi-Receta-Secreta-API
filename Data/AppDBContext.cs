using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Models;
using System.Data;

namespace MiRecetaSecretaAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) { }

        public DbSet<Ingredient> Ingredients { get; set; }

    }
}

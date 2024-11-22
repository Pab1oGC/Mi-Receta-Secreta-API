using Microsoft.EntityFrameworkCore;
using MiRecetaSecretaAPI.Models;

namespace MiRecetaSecretaAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) { }
        public DbSet<User> Users { get; set; }

    }
}

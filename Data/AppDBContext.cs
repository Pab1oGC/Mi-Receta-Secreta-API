using Microsoft.EntityFrameworkCore;

namespace MiRecetaSecretaAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options) { }


    }
}

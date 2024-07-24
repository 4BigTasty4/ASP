using Microsoft.EntityFrameworkCore;

namespace HM3
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-9UKOEVN;Database=MinimalApi;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Hotel> Hotels => Set<Hotel>();

    }
}

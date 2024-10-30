using Microsoft.EntityFrameworkCore;

namespace WebApp2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<ComponentProduct> ComponentProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=Teeradet; Database=TestAllDB2567; Trusted_Connection=True; TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComponentProduct>()
                .HasKey(key => new { key.ProductId, key.ComponentId });
        }

    }
}

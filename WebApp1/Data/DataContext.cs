using Microsoft.EntityFrameworkCore;

namespace WebApp1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=Teeradet; Database=TestDB2567; Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Random rnd = new Random();
            var products = new List<Product>();

            for (int i = 1; i <= 20; i++) 
            {
                products.Add(new Product
                {
                    Id = i,
                    Name = "MicroPhone" + i,
                    Price = rnd.Next(10, 99),
                    Amount = rnd.Next(1,10)
                });
            }

            modelBuilder.Entity<Product>().HasData(products);
        }

    }
}

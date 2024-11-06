

namespace WebApp5.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=Teeradet; Database=TestProductDB2567; Trusted_Connection=True; TrustServerCertificate=True");

        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}

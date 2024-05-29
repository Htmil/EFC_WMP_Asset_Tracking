using Microsoft.EntityFrameworkCore;

namespace EFC_WMP_Asset_Tracking
{
    internal class MyDbContext : DbContext
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFCAssetTrackerWMP;Integrated Security=True";

        public DbSet<Computer> Computers { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<Computer>(entity =>
            {
                entity.Property(e => e.PriceUSD)
                      .HasPrecision(18, 2); // Specify the precision and scale
            });

            // Configure the precision for the PriceUSD property in the Phone entity
            ModelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.PriceUSD)
                      .HasPrecision(18, 2); // Specify the precision and scale
            });

            ModelBuilder.Entity<Computer>().HasData(new Computer { Id = 1, Type = "Computer", Brand = "ASUS ROG", Model = "B550-F", Office = "Sweden", PurchaseDate = new DateOnly(2020, 11, 24), PriceUSD = 243, Currency = "SEK" });
            ModelBuilder.Entity<Computer>().HasData(new Computer { Id = 2, Type = "Computer", Brand = "HP", Model = "14S-FQ1010NO", Office = "USA", PurchaseDate = new DateOnly(2022, 01, 30), PriceUSD = 679, Currency = "USD" });
            ModelBuilder.Entity<Computer>().HasData(new Computer { Id = 3, Type = "Computer", Brand = "HP", Model = "Elitebook", Office = "Greece", PurchaseDate = new DateOnly(2021, 08, 30), PriceUSD = 2234, Currency = "EUR" });
            ModelBuilder.Entity<Computer>().HasData(new Computer { Id = 4, Type = "Computer", Brand = "HP", Model = "Elitebook", Office = "Sweden", PurchaseDate = new DateOnly(2020, 07, 30), PriceUSD = 2234, Currency = "SEK" });

            ModelBuilder.Entity<Phone>().HasData(new Phone { Id = 5, Type = "Phone", Brand = "Samsung", Model = "S20 Plus", Office = "Sweden", PurchaseDate = new DateOnly(2020, 09, 12), PriceUSD = 1500, Currency = "SEK" });
            ModelBuilder.Entity<Phone>().HasData(new Phone { Id = 6, Type = "Phone", Brand = "Sony Xperia", Model = "10 III", Office = "USA", PurchaseDate = new DateOnly(2020, 03, 06), PriceUSD = 800, Currency = "USD" });
            ModelBuilder.Entity<Phone>().HasData(new Phone { Id = 7, Type = "Phone", Brand = "Iphone", Model = "10", Office = "Greece", PurchaseDate = new DateOnly(2018, 11, 25), PriceUSD = 951, Currency = "EUR" });
        }
    }
}
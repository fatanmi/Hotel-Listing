using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Listing.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "United States",
                ShortName = "USA"
            },
            new Country
            {
                Id = 2,
                Name = "France",
                ShortName = "FR"
            },
            new Country
            {
                Id = 3,
                Name = "Japan",
                ShortName = "JP"
            },
            new Country
            {
                Id = 4,
                Name = "Australia",
                ShortName = "AUS"
            }
        );

            modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Sunset Resort",
                Address = "123 Beachfront Ave, Miami, FL",
                Rating = 4.5,
                CountryId = 1,
            },
            new Hotel
            {
                Id = 2,
                Name = "Mountain View Inn",
                Address = "456 Alpine St, France, FR",
                Rating = 4.2,
                CountryId = 2,
            },
            new Hotel
            {
                Id = 3,
                Name = "Australia Elegance",
                Address = "789 Rue de Aus, Aus, Australia",
                Rating = 4.8,
                CountryId = 2,
            },
            new Hotel
            {
                Id = 4,
                Name = "Tokyo Stay",
                Address = "101 Shinjuku, Tokyo, Japan",
                Rating = 4.7,
                CountryId = 3,
            });
        }
      
    }
}

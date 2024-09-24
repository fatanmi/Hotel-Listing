using Hotel_Listing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel_Listing.Properties.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}

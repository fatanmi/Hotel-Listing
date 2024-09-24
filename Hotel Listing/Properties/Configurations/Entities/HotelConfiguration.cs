using Hotel_Listing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel_Listing.Properties.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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

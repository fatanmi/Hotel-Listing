using AutoMapper;
using Hotel_Listing.Data;
using Hotel_Listing.Models;

namespace Hotel_Listing.Properties.Configurations
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer() {

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
            //CreateMap<ApiUser, LoginUserDTO>().ReverseMap();
        }

    }
}

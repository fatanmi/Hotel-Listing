using Hotel_Listing.Models;

namespace Hotel_Listing.Services
{
    public interface IAuthManager {

        Task<bool> ValidateUser(LoginUserDTO loginUser);

        Task<string> CreateToken();
    }
}

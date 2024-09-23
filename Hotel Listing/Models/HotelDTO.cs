using Hotel_Listing.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Listing.Models
{
    public class CreateHotelDTO 
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Hotel Name is too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Hotel Address is too long")]
        public string Address { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }
        public int CountryId { get; set; }
       
    }
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        //public HotelDTO Hotels { get; set; }

    }
}

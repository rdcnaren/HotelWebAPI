using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.DTO
{
    public class HotelAmenitiesCreateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public int HotelId { get; set; }

    }
}

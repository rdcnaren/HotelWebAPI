using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHotel.DTO
{
    public class HotelAmenitiesDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
    }
}

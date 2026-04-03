using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWeb_API.Models.DTO
{
    public class HotelAmenitiesCreateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public int HotelId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWeb_API.Models
{
    public class HotelAmenities
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Required]
        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}

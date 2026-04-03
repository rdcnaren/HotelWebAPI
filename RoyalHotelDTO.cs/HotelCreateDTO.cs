using System.ComponentModel.DataAnnotations;

namespace FirstWeb_API.Models.DTO
{
    public class HotelCreateDTO
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Details { get; set; }
        public double Rate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

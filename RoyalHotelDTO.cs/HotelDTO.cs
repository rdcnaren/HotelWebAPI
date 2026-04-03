using System.ComponentModel.DataAnnotations;

namespace FirstWeb_API.Models.DTO
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Details { get; set; }
        public double Rate { get; set; }
        public string? ImageUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.DTO
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

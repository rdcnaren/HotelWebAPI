using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.DTO
{
    public class HotelUpdateDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Details { get; set; }
        public double Rate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

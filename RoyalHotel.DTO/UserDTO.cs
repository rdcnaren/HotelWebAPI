using System.ComponentModel.DataAnnotations;

namespace RoyalHotel.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }       
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

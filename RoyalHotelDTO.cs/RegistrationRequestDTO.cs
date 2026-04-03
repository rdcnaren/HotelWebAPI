using System.ComponentModel.DataAnnotations;

namespace FirstWeb_API.Models.DTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

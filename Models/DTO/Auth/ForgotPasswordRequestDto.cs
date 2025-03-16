using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO.Auth
{
    public class ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? ClientUrl { get; set; }
    }
}

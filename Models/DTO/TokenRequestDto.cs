using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO
{
    public class TokenRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}

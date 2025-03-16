using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}

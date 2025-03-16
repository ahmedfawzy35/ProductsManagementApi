using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO
{
    public class AssignRoleDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DefaultValue("user@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [DefaultValue("User")]
        public string Role { get; set; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO.Auth
{
    public class AddUserDto : RequestRegisterDto
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string Role { get; set; }
    }
}
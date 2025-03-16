using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Models.DTO
{
    public class RegisterLoginDto
    {
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

namespace Products_Management_API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public List<string> Roles {  get; set; }
    }
}

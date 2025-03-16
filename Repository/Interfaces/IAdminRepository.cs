using Microsoft.AspNetCore.Identity;
using Products_Management_API.Data;
using Products_Management_API.Models.DTO.Auth;

namespace Products_Management_API.Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<List<ApplicationUser>> GetUsersByRoleAsync(string roleName);
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<string> UpdateUserRoleAsync(string userId, string newRole);
        Task<string> DeleteUserAsync(string userId);
    }
}

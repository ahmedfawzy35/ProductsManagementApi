using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Category;

namespace Products_Management_API.Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetCategoriesAddedInLastDay(int days);
        Task<IEnumerable<CategoryProductCountDto>> GetCategoryProductCountsAsync();
        Task<IEnumerable<Category>> GetFilteredCategoriesAsync(string? name, bool? isActive, DateTime? startDate, DateTime? endDate);
    }
}

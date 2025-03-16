using Products_Management_API.Models.DTO.Category;

namespace Products_Management_API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByNameAsync(string name);
        Task<CategoryDto> GetByIdAsync(int id);
        Task AddAsync(CategoryCreateDto createDto);
        Task UpdateAsync(int id, CategoryUpdateDto updateDto);
        Task DeleteAsync(int id);

        Task<IEnumerable<CategoryDto>> GetCategoriesAddedInLastDay(int days);

        Task<IEnumerable<CategoryProductCountDto>> GetCategoryProductCountsAsync();

        Task<IEnumerable<CategoryDto>> GetFilteredCategoriesAsync(string? name, bool? isActive, DateTime? startDate, DateTime? endDate);
    }
}

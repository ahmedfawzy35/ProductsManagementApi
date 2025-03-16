using Products_Management_API.Models.DTO.Product;

namespace Products_Management_API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task AddAsync(CreateProductDto createDto);
        Task UpdateAsync(int id, UpdateProductDto updateDto);
        Task DeleteAsync(int id);

        Task<IEnumerable<ProductDto>> GetCategoriesAddedInLastDay(int days);

        Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filterDto);

        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
    }
}

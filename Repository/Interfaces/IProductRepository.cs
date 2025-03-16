using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Product;

namespace Products_Management_API.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductAsync(int categoryId);

        Task<IEnumerable<ProductDto>> GetProductsAddedInLastDay(int days);

        Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filterDto);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);

        Task<bool> CategoryExistsAsync(int categoryId);
        Task<bool> SupplierExistsAsync(int supplierId);
    }
}

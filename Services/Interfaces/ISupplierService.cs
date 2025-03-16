using Products_Management_API.Models.DTO.Supplier;

namespace Products_Management_API.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto> GetByIdAsync(int id);
        Task AddAsync(CreateSupplierDto entity);
        Task UpdateAsync(int id, UpdateSupplierDto entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<SupplierDto>> GetSuppliersAddedInLastDay(int days);

        Task<IEnumerable<SupplierDto>> GetFilteredSuppliersAsync(SupplierFilterDto filter);
    }
}

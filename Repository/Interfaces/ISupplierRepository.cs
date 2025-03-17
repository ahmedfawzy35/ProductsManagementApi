using Products_Management_API.Models.Domain;
using ProductsManagement.Models.DTO.Supplier;

namespace Products_Management_API.Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetSuppliersAddedInLastDay(int days);
        Task<IEnumerable<Supplier>> GetFilteredSuppliersAsync(SupplierFilterDto filterDto);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneExistsAsync(string phoneNumber);
    }
}

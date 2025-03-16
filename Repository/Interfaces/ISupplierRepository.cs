using Products_Management_API.Models.Domain;

namespace Products_Management_API.Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetSuppliersAddedInLastDay(int days);
        Task<IEnumerable<Supplier>> GetFilteredSuppliersAsync(SupplierFilterDto filter);
    }
}

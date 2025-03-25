using Products_Management_API.Models.DTO.Customer;
using ProductsManagement.Models.Domain;

namespace Products_Management_API.Repository.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersBornAfterYearAsync(int year);
        Task<IEnumerable<Customer>> GetFilteredCustomersAsync(CustomerFilterDto filterDto);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneExistsAsync(string phoneNumber);
    }
}


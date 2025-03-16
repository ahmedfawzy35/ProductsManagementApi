using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Customer;

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


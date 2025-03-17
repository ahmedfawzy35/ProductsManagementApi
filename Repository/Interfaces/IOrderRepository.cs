using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Order;
using ProductsManagement.Models.DTO.Order;

namespace Products_Management_API.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersAddedInLastDay(int days);

        Task<IEnumerable<OrderDto>> GetFilteredOrdersAsync(OrderFilterDto filterDto);
        Task<bool> CustomerExistsAsync(int customerId);
    }
}

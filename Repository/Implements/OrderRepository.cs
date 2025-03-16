using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Order;
using Products_Management_API.Repository.Interfaces;

namespace Products_Management_API.Repository.Implements
{
    public class OrderRepository(AppDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<OrderDto>> GetOrdersAddedInLastDay(int days)
        {
            var fromTime = DateTime.Now.AddDays(-days);
            return await _dbContext.Orders
                .AsNoTrackingWithIdentityResolution()
                .Where(o => o.OrderDate >= fromTime)
                 .Select(order => new OrderDto
                 {
                     Id = order.Id,
                     OrderDate = order.OrderDate,
                     TotalAmount = order.TotalAmount,
                     OrderStatus = order.OrderStatus,
                     PaymentMethod = order.PaymentMethod,
                     ShippingAddress = order.ShippingAddress,
                     CustomerId = order.CustomerId,
                     CustomerName = _dbContext.Customers
                        .Where(c => c.Id == order.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault() ?? "Unknown"
                 })
                .OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _dbContext.Orders
                .AsNoTrackingWithIdentityResolution()
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus,
                    PaymentMethod = order.PaymentMethod,
                    ShippingAddress = order.ShippingAddress,
                    CustomerId = order.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == order.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault() ?? "Unknown"
                })
                .ToListAsync();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _dbContext.Orders
                .AsNoTrackingWithIdentityResolution()
                .Where(o => o.Id == id)
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus,
                    PaymentMethod = order.PaymentMethod,
                    ShippingAddress = order.ShippingAddress,
                    CustomerId = order.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == order.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault() ?? "Unknown"
                }).FirstOrDefaultAsync();

            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetFilteredOrdersAsync(OrderFilterDto filter)
        {
            var query = _dbContext.Orders.AsQueryable();

            if (filter.OrderDateFrom.HasValue)
                query = query.Where(o => o.OrderDate >= filter.OrderDateFrom.Value);

            if (filter.OrderDateTo.HasValue)
                query = query.Where(o => o.OrderDate <= filter.OrderDateTo.Value);

            if (filter.MinTotalAmount.HasValue)
                query = query.Where(o => o.TotalAmount >= filter.MinTotalAmount.Value);

            if (filter.MaxTotalAmount.HasValue)
                query = query.Where(o => o.TotalAmount <= filter.MaxTotalAmount.Value);

            if (!string.IsNullOrWhiteSpace(filter.OrderStatus))
                query = query.Where(o => o.OrderStatus == filter.OrderStatus);

            if (!string.IsNullOrWhiteSpace(filter.PaymentMethod))
                query = query.Where(o => o.PaymentMethod == filter.PaymentMethod);

            if (!string.IsNullOrWhiteSpace(filter.ShippingAddress))
                query = query.Where(o => o.ShippingAddress.Contains(filter.ShippingAddress));

            if (filter.CustomerId.HasValue)
                query = query.Where(o => o.CustomerId == filter.CustomerId.Value);

            // Sorting
            query = filter.SortBy?.ToLower() switch
            {
                "date" => filter.IsAscending ? query.OrderBy(o => o.OrderDate) : query.OrderByDescending(o => o.OrderDate),
                "amount" => filter.IsAscending ? query.OrderBy(o => o.TotalAmount) : query.OrderByDescending(o => o.TotalAmount),
                _ => query
            };

            return await query.Select(o => new OrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                PaymentMethod = o.PaymentMethod,
                ShippingAddress = o.ShippingAddress,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer != null ? o.Customer.FirstName + " " + o.Customer.LastName : "N/A"
            }).ToListAsync();
        }


        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            return await _dbContext.Customers.AnyAsync(c => c.Id == customerId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Models.Domain;
using Products_Management_API.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Repository.Implements
{
    public class SupplierRepository(AppDbContext dbContext) : GenericRepository<Supplier>(dbContext), ISupplierRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<Supplier>> GetSuppliersAddedInLastDay(int days)
        {
            if (days <= 0)
            {
                throw new ValidationException("Day must be greater than zero.");
            }

            var fromDate = DateTime.Now.AddDays(-days);

            return await _dbContext.Suppliers
                .Where(p => p.CreatedDate >= fromDate)
                .OrderByDescending(p => p.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Supplier>> GetFilteredSuppliersAsync(SupplierFilterDto filter)
        {
            var query = _dbContext.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(s => s.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(s => s.Email == filter.Email);

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
                query = query.Where(s => s.PhoneNumber.Contains(filter.PhoneNumber));

            if (!string.IsNullOrWhiteSpace(filter.Address))
                query = query.Where(s => s.Address.Contains(filter.Address));

            if (!string.IsNullOrWhiteSpace(filter.CompanyName))
                query = query.Where(s => s.CompanyName.Contains(filter.CompanyName));

            if (filter.CreatedDateFrom.HasValue)
                query = query.Where(s => s.CreatedDate >= filter.CreatedDateFrom.Value);

            if (filter.CreatedDateTo.HasValue)
                query = query.Where(s => s.CreatedDate <= filter.CreatedDateTo.Value);

            if (filter.IsActive.HasValue)
                query = query.Where(s => s.IsActive == filter.IsActive.Value);

            return await query.ToListAsync();
        }
    }
}

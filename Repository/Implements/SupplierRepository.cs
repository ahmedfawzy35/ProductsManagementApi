using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Models.Domain;
using Products_Management_API.Repository.Interfaces;
using ProductsManagement.Models.DTO.Supplier;
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

        public async Task<IEnumerable<Supplier>> GetFilteredSuppliersAsync(SupplierFilterDto filterDto)
        {
            var query = _dbContext.Suppliers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterDto.Name))
                query = query.Where(s => s.Name.Contains(filterDto.Name));

            if (!string.IsNullOrWhiteSpace(filterDto.Email))
                query = query.Where(s => s.Email == filterDto.Email);

            if (!string.IsNullOrWhiteSpace(filterDto.PhoneNumber))
                query = query.Where(s => s.PhoneNumber.Contains(filterDto.PhoneNumber));

            if (!string.IsNullOrWhiteSpace(filterDto.Address))
                query = query.Where(s => s.Address.Contains(filterDto.Address));

            if (!string.IsNullOrWhiteSpace(filterDto.CompanyName))
                query = query.Where(s => s.CompanyName.Contains(filterDto.CompanyName));

            if (filterDto.CreatedDateFrom.HasValue)
                query = query.Where(s => s.CreatedDate >= filterDto.CreatedDateFrom.Value);

            if (filterDto.CreatedDateTo.HasValue)
                query = query.Where(s => s.CreatedDate <= filterDto.CreatedDateTo.Value);

            if (filterDto.IsActive.HasValue)
                query = query.Where(s => s.IsActive == filterDto.IsActive.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Suppliers
                 .AnyAsync(s => s.Email.ToUpper() == email.ToUpper());
        }

        public async Task<bool> PhoneExistsAsync(string phoneNumber)
        {
            return await _dbContext.Suppliers
                .AnyAsync(p => p.PhoneNumber == phoneNumber);
        }
    }
}

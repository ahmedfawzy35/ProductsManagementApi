using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Enums;
using Products_Management_API.Models.DTO.Product;
using Products_Management_API.Repository.Interfaces;
using ProductsManagement.Models.Domain;

namespace Products_Management_API.Repository.Implements
{
    public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _dbContext.Products
     .AsNoTracking()
     .Select(p => new ProductDto
     {
         Id = p.Id,
         Name = p.Name,
         Description = p.Description,
         Price = p.Price,
         StockQuantity = p.StockQuantity,
         CreatedDate = p.CreatedDate,
         IsAvailable = p.IsAvailable,
         ImagePath = p.ImagePath,
         CategoryId = p.CategoryId,
         CategoryName = _dbContext.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault(),
         SupplierId = p.SupplierId,
         SupplierName = _dbContext.Suppliers
                        .Where(s => s.Id == p.SupplierId)
                        .Select(s => s.Name)
                        .FirstOrDefault()
     })
     .ToListAsync();

        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            var product = await _dbContext.Products
                .AsNoTrackingWithIdentityResolution()
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CreatedDate = p.CreatedDate,
                    IsAvailable = p.IsAvailable,
                    ImagePath = p.ImagePath,
                    CategoryId = p.CategoryId,
                    CategoryName = _dbContext.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault(),
                    SupplierId = p.SupplierId,
                    SupplierName = _dbContext.Suppliers
                        .Where(s => s.Id == p.SupplierId)
                        .Select(s => s.Name)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAddedInLastDay(int days)
        {
            var fromDate = DateTime.Now.AddDays(-days);

            return await _dbContext.Products
                .AsNoTrackingWithIdentityResolution()
                .Where(p => p.CreatedDate >= fromDate)
                 .Select(p => new ProductDto
                 {
                     Id = p.Id,
                     Name = p.Name,
                     Description = p.Description,
                     Price = p.Price,
                     StockQuantity = p.StockQuantity,
                     CreatedDate = p.CreatedDate,
                     IsAvailable = p.IsAvailable,
                     ImagePath = p.ImagePath,
                     CategoryId = p.CategoryId,
                     CategoryName = _dbContext.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault(),
                     SupplierId = p.SupplierId,
                     SupplierName = _dbContext.Suppliers
                        .Where(s => s.Id == p.SupplierId)
                        .Select(s => s.Name)
                        .FirstOrDefault()
                 })
                .OrderByDescending(p => p.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetFilteredProductsAsync(ProductFilterDto filterDto)
        {
            var query = _dbContext.Products.AsQueryable();

            if (filterDto.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filterDto.MinPrice);

            if (filterDto.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filterDto.MaxPrice);

            if (filterDto.IsAvailable.HasValue)
                query = query.Where(p => p.IsAvailable == filterDto.IsAvailable.Value);

            if (filterDto.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filterDto.CategoryId);


            query = filterDto.OrderBy.ToLower() switch
            {
                "price" => filterDto.OrderDirection == OrderDirection.Ascending
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),
                "name" => filterDto.OrderDirection == OrderDirection.Ascending
                    ? query.OrderBy(p => p.Name)
                    : query.OrderByDescending(p => p.Name),
                "date" => filterDto.OrderDirection == OrderDirection.Ascending
                    ? query.OrderBy(p => p.CreatedDate)
                    : query.OrderByDescending(p => p.CreatedDate),
                "quantity" => filterDto.OrderDirection == OrderDirection.Ascending
                    ? query.OrderBy(p => p.StockQuantity)
                    : query.OrderByDescending(p => p.StockQuantity),
                _ => query
            };

            return await query
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CreatedDate = p.CreatedDate,
                    IsAvailable = p.IsAvailable,
                    ImagePath = p.ImagePath,
                    CategoryId = p.CategoryId,
                    CategoryName = _dbContext.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault(),
                    SupplierId = p.SupplierId,
                    SupplierName = _dbContext.Suppliers
                        .Where(s => s.Id == p.SupplierId)
                        .Select(s => s.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CreatedDate = p.CreatedDate,
                    IsAvailable = p.IsAvailable,
                    ImagePath = p.ImagePath,
                    CategoryId = p.CategoryId,
                    CategoryName = _dbContext.Categories
                        .Where(c => c.Id == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault(),
                    SupplierId = p.SupplierId,
                    SupplierName = _dbContext.Suppliers
                        .Where(s => s.Id == p.SupplierId)
                        .Select(s => s.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _dbContext.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<bool> SupplierExistsAsync(int supplierId)
        {
            return await _dbContext.Suppliers.AnyAsync(c => c.Id == supplierId);
        }
    }
}

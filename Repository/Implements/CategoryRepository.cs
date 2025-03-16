using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Category;
using Products_Management_API.Repository.Interfaces;

namespace Products_Management_API.Repository.Implements
{
    public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<Category> GetByNameAsync(string name)
        {
            var category = await _dbContext.Set<Category>()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAddedInLastDay(int days)
        {
            var fromDate = DateTime.Now.AddDays(-days);

            return await _dbContext.Categories
                .Where(c => c.CreatedDate >= fromDate)
                .OrderByDescending(c => c.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<CategoryProductCountDto>> GetCategoryProductCountsAsync()
        {
            return await _dbContext.Categories
       .Include(c => c.Products)
       .Select(c => new CategoryProductCountDto
       {
           CategoryId = c.Id,
           CategoryName = c.Name,
           ProductCount = c.Products.Count
       })
       .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetFilteredCategoriesAsync(string? name, bool? isActive, DateTime? startDate, DateTime? endDate)
        {
            var query = _dbContext.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(c => c.CreatedDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(c => c.CreatedDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }
    }
}

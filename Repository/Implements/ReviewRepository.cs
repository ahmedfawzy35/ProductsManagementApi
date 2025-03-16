using Microsoft.EntityFrameworkCore;
using Products_Management_API.Data;
using Products_Management_API.Enums;
using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Review;
using Products_Management_API.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Products_Management_API.Repository.Implements
{
    public class ReviewRepository(AppDbContext dbContext) : GenericRepository<Review>(dbContext), IReviewRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            return await _dbContext.Reviews
                .AsNoTrackingWithIdentityResolution()
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    ProductId = r.ProductId,
                    ProductName = _dbContext.Products
                        .Where(p => p.Id == r.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    CustomerId = r.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == r.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _dbContext.Reviews
                .AsNoTrackingWithIdentityResolution()
                .Where(r => r.Id == id)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    ProductId = r.ProductId,
                    ProductName = _dbContext.Products
                        .Where(p => p.Id == r.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    CustomerId = r.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == r.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

            return review;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAddedInLastDay(int days)
        {
            if (days <= 0)
            {
                throw new ValidationException("Day must be greater than zero.");
            }
            var fromDate = DateTime.Now.AddDays(-days);

            return await _dbContext.Reviews
                .AsNoTrackingWithIdentityResolution()
                .Where(p => p.ReviewDate >= fromDate)
                .OrderByDescending(p => p.ReviewDate)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    ProductId = r.ProductId,
                    ProductName = _dbContext.Products
                        .Where(p => p.Id == r.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    CustomerId = r.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == r.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault()
                }).ToListAsync();
        }

        public async Task<IEnumerable<ReviewDto>> GetFilteredReviewsAsync(int? minRating, int? maxRating, string orderBy, OrderDirection orderDirection)
        {
            var query = _dbContext.Reviews.AsQueryable();

            if (minRating.HasValue)
                query = query.Where(r => r.Rating >= minRating.Value);

            if (maxRating.HasValue)
                query = query.Where(r => r.Rating <= maxRating.Value);

            // Apply ordering
            query = orderBy switch
            {
                "rating" => orderDirection == OrderDirection.Ascending
                    ? query.OrderBy(r => r.Rating)
                    : query.OrderByDescending(r => r.Rating),
                "date" => orderDirection == OrderDirection.Ascending
                    ? query.OrderBy(r => r.ReviewDate)
                    : query.OrderByDescending(r => r.ReviewDate),
                _ => throw new ArgumentException($"Invalid orderBy value: {orderBy}")
            };

            return await query
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    ProductId = r.ProductId,
                    ProductName = _dbContext.Products
                        .Where(p => p.Id == r.ProductId)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    CustomerId = r.CustomerId,
                    CustomerName = _dbContext.Customers
                        .Where(c => c.Id == r.CustomerId)
                        .Select(c => c.FirstName + " " + c.LastName)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}

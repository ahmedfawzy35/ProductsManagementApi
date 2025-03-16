using Products_Management_API.Enums;
using Products_Management_API.Models.Domain;
using Products_Management_API.Models.DTO.Review;

namespace Products_Management_API.Repository.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
        Task<ReviewDto> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDto>> GetReviewsAddedInLastDay(int days);

        Task<IEnumerable<ReviewDto>> GetFilteredReviewsAsync(int? minRating, int? maxRating, string orderBy, OrderDirection orderDirection);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products_Management_API.Enums;
using Products_Management_API.Models.DTO.Review;
using Products_Management_API.Repository.Interfaces;
using Products_Management_API.Services.Interfaces;

namespace Products_Management_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ReviewController(IReviewService reviewService, IReviewRepository review, IMapper mapper) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;
        private readonly IReviewRepository _review = review;
        private readonly IMapper _mapper = mapper;

        [HttpGet("AllReviews")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reviewDto = await _reviewService.GetAllAsync();
                return Ok(reviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ReviewById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reviewDto = await _reviewService.GetByIdAsync(id);
                return Ok(reviewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> Create(CreateReviewDto createReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _reviewService.AddAsync(createReview);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpPut("UpdateReview/{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateReviewDto updateReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _reviewService.UpdateAsync(id, updateReview);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteReview/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _reviewService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllReviewsAddedInLast/{days}Days")]
        public async Task<IActionResult> GetAllReviewsAddedInLastDays(int days)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reviews = await _review.GetReviewsAddedInLastDay(days);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filtering")]
        public async Task<IActionResult> GetFilteredReviewsAsync([FromQuery] int? minRating, [FromQuery] int? maxRating, [FromQuery] string orderBy, [FromQuery] OrderDirection orderDirection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reviews = await _review.GetFilteredReviewsAsync(minRating, maxRating, orderBy, orderDirection);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

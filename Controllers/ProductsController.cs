using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products_Management_API.Models.DTO.Product;
using Products_Management_API.Services.Interfaces;
using System.Security.Claims;

namespace Products_Management_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("AllProducts")]
        //[Authorize]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productsDto = await _productService.GetAllAsync();
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("ProductById/{id:int}")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var productDto = await _productService.GetByIdAsync(id);
                return Ok(new { p = productDto, Id = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ProductByCategoryId/{categoryId:int}")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productsDto = await _productService.GetProductsByCategoryIdAsync(categoryId);
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> Add([FromForm] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _productService.AddAsync(createProductDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productDto = await _productService.GetByIdAsync(id);

                await _productService.UpdateAsync(id, updateDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllProductAddedInLast/{days:int}Days")]
        public async Task<IActionResult> GetAllProductAddedInLastDays(int days)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var categoriesDto = await _productService.GetCategoriesAddedInLastDay(days);
                return Ok(categoriesDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Filtering")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] ProductFilterDto filterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var productsDto = await _productService.GetFilteredProductsAsync(filterDto);

                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

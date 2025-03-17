using Microsoft.AspNetCore.Mvc;
using Products_Management_API.Models.DTO.Supplier;
using Products_Management_API.Services.Interfaces;
using ProductsManagement.Models.DTO.Supplier;

namespace Products_Management_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class SupplierController(ISupplierService supplierService) : ControllerBase
    {
        private readonly ISupplierService _supplierService = supplierService;

        [HttpGet("AllSupplier")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var supplier = await _supplierService.GetAllAsync();

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SupplierById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("AddSupplier")]
        public async Task<IActionResult> Create(CreateSupplierDto createSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _supplierService.AddAsync(createSupplier);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSupplier/{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateSupplierDto updateSupplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _supplierService.UpdateAsync(id, updateSupplier);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteSupplier/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _supplierService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllSuppliersAddedInLast/{days:int}Days")]
        public async Task<IActionResult> GetAllReviewsAddedInLastDays(int days)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var suppliersDto = await _supplierService.GetSuppliersAddedInLastDay(days);
                return Ok(suppliersDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filtering")]
        public async Task<IActionResult> GetFilteredSuppliersAsync([FromQuery] SupplierFilterDto filterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var suppliers = await _supplierService.GetFilteredSuppliersAsync(filterDto);
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

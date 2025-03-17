using Microsoft.AspNetCore.Mvc;
using Products_Management_API.Models.DTO.Order;
using Products_Management_API.Services.Interfaces;
using ProductsManagement.Models.DTO.Order;

namespace Products_Management_API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet("AllOrders")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ordersDto = await _orderService.GetAllOrdersAsync();
                return Ok(ordersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("OrderById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var orderDto = await _orderService.GetOrderByIdAsync(id);
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddOrder")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Add(OrderCreateDto orderCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _orderService.AddAsync(orderCreateDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateOrder/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _orderService.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteOrder/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("AllOrdersAddedInLast/{days:int}Days")]
        public async Task<IActionResult> GetAllProductAddedInLastDays(int days)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var categoriesDto = await _orderService.GetOrdersAddedInLastDay(days);
                return Ok(categoriesDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filtering")]
        public async Task<IActionResult> GetFilteredOrders([FromQuery] OrderFilterDto filterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ordersDto = await _orderService.GetFilteredOrdersAsync(filterDto);

                return Ok(ordersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

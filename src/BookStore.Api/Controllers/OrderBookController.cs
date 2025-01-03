using BookStore.Dto.Request;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderBookController : ControllerBase
    {
        private readonly IOrderBookService service;

        public OrderBookController(IOrderBookService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("orderId")]
        public async Task<IActionResult> GetOrderId(int orderId)
        {
            var response = await service.GetOrderIdAsync(orderId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("bookId")]
        public async Task<IActionResult> GetBookId(int bookId)
        {
            var response = await service.GetBookIdAsync(bookId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(OrderBookRequestDto orderBookRequestDto)
        {
            var response = await service.AddAsync(orderBookRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, OrderBookRequestDto orderBookRequestDto)
        {
            var response = await service.UpdateAsync(id, orderBookRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

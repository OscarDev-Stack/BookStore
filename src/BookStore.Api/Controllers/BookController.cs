using BookStore.Dto.Request;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;

        public BookController(IBookService service) 
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PaginationRequestDto pagination)
        {
            var response = await service.GetAsync(pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetName(string? name, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetNameAsync(name, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("author")]
        public async Task<IActionResult> GetAuthor(string? author, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetAuthorAsync(author, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(BookRequestDto book)
        {
            var response = await service.AddAsync(book);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, BookRequestDto book)
        {
            var response = await service.UpdateAsync(id, book);
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

using BookStore.Dto.Request;
using BookStore.Entities;
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
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]PaginationRequestDto pagination)
        {
            var response = await service.GetAsync(pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("name")]
        [AllowAnonymous]
        public async Task<IActionResult> GetName(string? name, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetNameAsync(name, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("author")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthor(string? author, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetAuthorAsync(author, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("ISBN")]
        [AllowAnonymous]
        public async Task<IActionResult> GetISBN(string? ISBN, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetISBNAsync(ISBN, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Post(BookRequestDto book)
        {
            var response = await service.AddAsync(book);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Put(int id, BookRequestDto book)
        {
            var response = await service.UpdateAsync(id, book);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

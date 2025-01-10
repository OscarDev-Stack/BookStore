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
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetName(string? search, [FromQuery] PaginationRequestDto pagination)
        {
            var response = await service.GetAsync(search, pagination);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Post([FromForm]BookRequestDto book)
        {
            var response = await service.AddAsync(book);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Put(int id,[FromForm] BookRequestDto book)
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

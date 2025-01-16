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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return Ok(response);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return Ok(response);
        }
        [HttpGet("fullName")]
        public async Task<IActionResult> Get(string? fullName)
        {
            var response = await service.GetAsync(fullName);
            return Ok(response);
        }
        [HttpGet("DNI")]
        public async Task<IActionResult> GetCustomerDNI(string? dni)
        {
            var response = await service.GetCustomerDNIAsync(dni);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CustomerRequestDto customerRequestDto)
        {
            var response = await service.AddAsync(customerRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] CustomerRequestDto customerRequestDto)
        {
            var response = await service.UpdateAsync(id, customerRequestDto);
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

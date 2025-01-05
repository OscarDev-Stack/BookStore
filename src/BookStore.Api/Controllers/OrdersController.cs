using Azure;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService service;

        public OrdersController(IOrderService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return response.Success ? Ok(response) : BadRequest(response);
            //var ordersDb = await repository.GetAsync();
            //return Ok(ordersDb);
        }
        [HttpGet("dni")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> Get(string? dni)
        {
            var response = await service.GetAsync(dni);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpGet("customerId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> GetCustomerId(int customerId)
        {
            var response = await service.GetCustomerIdAsync(customerId);
            return response.Success ? Ok(response) : NotFound(response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Post(OrderRequestDto orderRequestDto)
        {
            var response = await service.AddAsync(orderRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }
        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> Put(int id, OrderRequestDto orderRequestDto)
        {
            var response = await service.UpdateAsync(id, orderRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
            //var response = new BaseResponse();
            //try
            //{
            //    var orderDb = await repository.GetAsync(id);
            //    if (orderDb is null)
            //    {
            //        response.ErrorMessage = "No se encontro información.";
            //        return NotFound(response);
            //    }
            //    orderDb.StartDate = orderRequestDto.StartDate;
            //    orderDb.CustomerId = orderRequestDto.CustomerId;
            //    orderDb.Status = orderRequestDto.Status;

            //    await repository.UpdateAsync();
            //    response.Success = true;
            //    return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Ocurrio un error al actualizar la información.";
            //    logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            //    return BadRequest(response);
            //}
        }
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Constants.RoleAdmin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
            //var response = new BaseResponse();
            //try
            //{
            //    var orderDb = await repository.GetAsync(id);
            //    if (orderDb is null)
            //    {
            //        response.ErrorMessage = "No se encontro información.";
            //        return NotFound(response);
            //    }
            //    await repository.DeleteAsync(id);
            //    response.Success = true;
            //    return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Ocurrio un error al eliminar la información.";
            //    logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            //    return BadRequest(response);
            //}
        }
        [HttpPut("id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{Constants.RoleLibrarian},{Constants.RoleAdmin}")]
        public async Task<IActionResult> FinalizeAsync(int id)
        {
            var response = await service.FinalizeAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

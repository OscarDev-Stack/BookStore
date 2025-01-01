using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(ICustomerRepository repository, ILogger<CustomerController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var customerDb = await repository.GetAsync();
                var customers = customerDb.Select(x => new CustomerResponseDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DNI = x.DNI,
                    Edad = x.Edad,
                    Status = x.Status
                }).ToList();
                response.Data = customers;
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<CustomerResponseDto>();
            try
            {
                var customerDb = await repository.GetAsync(id);
                if (customerDb is null)
                {
                    logger.LogWarning($"El cliente con id {id} no existe");
                    return NotFound(response);
                }
                else
                {
                    var customers = new CustomerResponseDto
                    {
                        Id = customerDb.Id,
                        FirstName= customerDb.FirstName,
                        LastName= customerDb.LastName,
                        DNI= customerDb.DNI,
                        Edad = customerDb.Edad,
                        Status = customerDb.Status
                    };

                    response.Data = customers;
                    response.Success = true;
                    return response.Data is not null ? Ok(response) : NotFound(response);

                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpGet("fullName")]
        public async Task<IActionResult> Get(string? fullName)
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var customerDb = await repository.GetAsync(x => (x.FirstName + " " + x.LastName).Contains(fullName ?? string.Empty));

                var customerDto = customerDb.Select(x => new CustomerResponseDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DNI= x.DNI,
                    Edad= x.Edad,
                    Status = x.Status
                }).ToList();

                response.Data = customerDto;
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpGet("DNI")]
        public async Task<IActionResult> GetCustomerDNI(string? DNI)
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var customerDb = await repository.GetAsync(x => x.DNI.Contains(DNI ?? string.Empty));

                var customerDto = customerDb.Select(x => new CustomerResponseDto
                {
                    Id = x.Id,
                    FirstName= x.FirstName,
                    LastName= x.LastName,
                    DNI = x.DNI,
                    Edad = x.Edad,
                    Status = x.Status
                }).ToList();

                response.Data = customerDto;
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(CustomerRequestDto customer)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var customerDb = new Customer()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DNI = customer.DNI,
                    Edad = customer.Edad,
                    Status = customer.Status
                };
                var CustomerId = await repository.AddAsync(customerDb);
                response.Success = true;
                response.Data = CustomerId;
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al guardar la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CustomerRequestDto customerRequestDto)
        {
            var response = new BaseResponse();
            try
            {
                var customerDb = await repository.GetAsync(id);
                if (customerDb is null)
                {
                    response.ErrorMessage = "No se encontro información.";
                    return NotFound(response);
                }

                customerDb.FirstName = customerRequestDto.FirstName;
                customerDb.LastName = customerRequestDto.LastName;
                customerDb.DNI = customerRequestDto.DNI;
                customerDb.Edad = customerRequestDto.Edad;
                customerDb.Status = customerRequestDto.Status;

                await repository.UpdateAsync();
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al actualizar la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();
            try
            {
                var customerDb = await repository.GetAsync(id);
                if (customerDb is null)
                {
                    response.ErrorMessage = "No se encontro información.";
                    return NotFound(response);
                }
                await repository.DeleteAsync(id);
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al eliminar la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
    }
}

using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IOrderRepository repository, IBookRepository bookRepository, ICustomerRepository customerRepository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.customerRepository = customerRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ordersDb = await repository.GetAsync();
            return Ok(ordersDb);
        }
        [HttpGet("dni")]
        public async Task<IActionResult> Get(string? dni)
        {
            var ordersDb = await repository.GetAsync(dni);
            return Ok(ordersDb);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<OrderResponseDto>();
            try
            {
                var orderDb = await repository.GetAsync(id);
                if (orderDb is null)
                {
                    logger.LogWarning($"El pedido con id {id} no existe");
                    return NotFound(response);
                }
                else
                {
                    var order = new OrderResponseDto
                    {
                        StartDate = orderDb.StartDate,
                        CustomerId = orderDb.CustomerId,
                        Customer = orderDb.Customer,
                        Status = orderDb.Status
                    };

                    response.Data = order;
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
        [HttpPost]
        public async Task<IActionResult> Post(OrderRequestDto orderRequestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var customer = await customerRepository.GetAsync(orderRequestDto.CustomerId);
                if(customer is null)
                {
                    response.ErrorMessage = $"El cliente con id: {orderRequestDto.CustomerId} es incorrecto.";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }

                var orderDb = new Order
                {
                    StartDate = orderRequestDto.StartDate,
                    CustomerId = orderRequestDto.CustomerId,
                    Status = orderRequestDto.Status
                };
                response.Data = await repository.AddAsync(orderDb);
                response.Success = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al guardar la información.";
                logger.LogError(ex, ex.Message);
                return BadRequest(response);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, OrderRequestDto orderRequestDto)
        {
            var response = new BaseResponse();
            try
            {
                var orderDb = await repository.GetAsync(id);
                if (orderDb is null)
                {
                    response.ErrorMessage = "No se encontro información.";
                    return NotFound(response);
                }
                orderDb.StartDate = orderRequestDto.StartDate;
                orderDb.CustomerId = orderRequestDto.CustomerId;
                orderDb.Status = orderRequestDto.Status;

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
                var orderDb = await repository.GetAsync(id);
                if (orderDb is null)
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

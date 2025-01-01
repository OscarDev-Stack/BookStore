using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Implementations;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderBookController : ControllerBase
    {
        private readonly IOrderBookRepository repository;
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ILogger<OrderBookController> logger;

        public OrderBookController(IOrderBookRepository repository, IBookRepository bookRepository, IOrderRepository orderRepository, ILogger<OrderBookController> logger)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderBooksDb = await repository.GetAsync();
            return Ok(orderBooksDb);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var orderBooks = await repository.GetIdAsync(id);
            return Ok(orderBooks);
            //var response = new BaseResponseGeneric<OrderBookResponseDto>();
            //try
            //{
            //    var orderBookDb = await repository.GetIdAsync(id);
            //    if (orderBookDb is null)
            //    {
            //        logger.LogWarning($"El libro asociado al pedido con id {id} no existe");
            //        return NotFound(response);
            //    }
            //    else
            //    {
            //        var orderBook = new OrderBookResponseDto
            //        {
            //            BookId = orderBookDb.BookId,
            //            OrderId = orderBookDb.OrderId,
            //            Status = orderBookDb.Status
            //        };

            //        response.Data = orderBook;
            //        response.Success = true;
            //        return response.Data is not null ? Ok(response) : NotFound(response);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Ocurrio un error al obtener la información.";
            //    logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            //    return BadRequest(response);
            //}
        }
        [HttpGet("orderId")]
        public async Task<IActionResult> GetOrderId(int orderId)
        {
            var response = new BaseResponseGeneric<ICollection<OrderBookResponseDto>>();
            try
            {
                var orderBookDb = await repository.GetAsync(x => x.OrderId == orderId);

                var orderBookDto = orderBookDb.Select(x => new OrderBookResponseDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    BookId = x.BookId,
                    Status = x.Status
                }).ToList();

                response.Data = orderBookDto;
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
        [HttpGet("bookId")]
        public async Task<IActionResult> GetBookId(int bookId)
        {
            var response = new BaseResponseGeneric<ICollection<OrderBookResponseDto>>();
            try
            {
                var orderBookDb = await repository.GetAsync(x => x.BookId == bookId);

                var orderBookDto = orderBookDb.Select(x => new OrderBookResponseDto
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    BookId = x.BookId,
                    Status = x.Status
                }).ToList();

                response.Data = orderBookDto;
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
        public async Task<IActionResult> Post(OrderBookRequestDto orderBookRequestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var order = await orderRepository.GetAsync(orderBookRequestDto.OrderId);
                if (order is null)
                {
                    response.ErrorMessage = $"El pedido con id: {orderBookRequestDto.OrderId} es incorrecto.";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }
                var book = await bookRepository.GetAsync(orderBookRequestDto.BookId);
                if (order is null)
                {
                    response.ErrorMessage = $"El libro con id: {orderBookRequestDto.OrderId} es incorrecto.";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }
                var orderBookDb = new OrderBook
                {
                    BookId = orderBookRequestDto.BookId,
                    OrderId = orderBookRequestDto.OrderId,
                    Status = orderBookRequestDto.Status
                };
                response.Data = await repository.AddAsync(orderBookDb);
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
        public async Task<IActionResult> Put(int id, OrderBookRequestDto orderBookRequestDto)
        {
            var response = new BaseResponse();
            try
            {
                var orderBookDb = await repository.GetAsync(id);
                if (orderBookDb is null)
                {
                    response.ErrorMessage = "No se encontro información.";
                    return NotFound(response);
                }
                orderBookDb.OrderId = orderBookRequestDto.OrderId;
                orderBookDb.BookId = orderBookRequestDto.BookId;
                orderBookDb.Status = orderBookRequestDto.Status;

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
                var orderBookDb = await repository.GetAsync(id);
                if (orderBookDb is null)
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

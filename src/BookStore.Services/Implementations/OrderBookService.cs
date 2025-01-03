using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Entities.Info;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations
{
    public class OrderBookService : IOrderBookService
    {
        private readonly IOrderBookRepository repository;
        private readonly IOrderRepository orderRepository;
        private readonly IBookRepository bookRepository;
        private readonly ILogger<OrderBookService> logger;
        private readonly IMapper mapper;

        public OrderBookService(IOrderBookRepository repository, IOrderRepository orderRepository, IBookRepository bookRepository,  ILogger<OrderBookService> logger, IMapper mapper) 
        {
            this.repository = repository;
            this.orderRepository = orderRepository;
            this.bookRepository = bookRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<OrderBookResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<OrderBookResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<OrderBookInfo>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<OrderBookInfo>();
            try
            {
                var data = await repository.GetIdAsync(id);
                response.Data = data; //mapper.Map<OrderBookInfo>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetBookIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ICollection<OrderBookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.BookId == id);
                response.Data = mapper.Map<ICollection<OrderBookResponseDto>>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetOrderIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ICollection<OrderBookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.OrderId == id);
                response.Data = mapper.Map<ICollection<OrderBookResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(OrderBookRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var order = await orderRepository.GetAsync(request.OrderId);
                var book = await bookRepository.GetAsync(request.BookId);
                if (order is null || book is null)
                {
                    if (order is null)
                    {
                        response.ErrorMessage = $"El pedido con id: {request.OrderId} es incorrecto.";
                        logger.LogWarning(response.ErrorMessage);
                    }
                    if (book is null)
                    {
                        response.ErrorMessage = $"El libro con id: {request.OrderId} es incorrecto.";
                        logger.LogWarning(response.ErrorMessage);
                    }
                }
                else
                {
                    var data = mapper.Map<OrderBook>(request);
                    var dataId = await repository.AddAsync(data);
                    response.Success = true;
                    response.Data = dataId;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is not null)
                {
                    await repository.DeleteAsync(id);
                    response.Success = true;
                }
                else response.ErrorMessage = "No se encontro información.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, OrderBookRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var order = await orderRepository.GetAsync(request.OrderId);
                var book = await bookRepository.GetAsync(request.BookId);
                if (order is null || book is null)
                {
                    if (order is null)
                    {
                        response.ErrorMessage = $"El pedido con id: {request.OrderId} es incorrecto.";
                        logger.LogWarning(response.ErrorMessage);
                    }
                    if (book is null)
                    {
                        response.ErrorMessage = $"El libro con id: {request.BookId} es incorrecto.";
                        logger.LogWarning(response.ErrorMessage);
                    }
                }
                else
                {
                    var data = await repository.GetAsync(id);
                    if (data is not null)
                    {
                        mapper.Map(request, data);
                        await repository.UpdateAsync();
                        response.Success = true;
                    }
                    else response.ErrorMessage = "No se encontro información.";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
    }
}

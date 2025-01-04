using AutoMapper;
using Azure.Core;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<OrderService> logger;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, ICustomerRepository customerRepository, ILogger<OrderService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<OrderResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<OrderResponseDto>>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<OrderResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<OrderResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<OrderResponseDto>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderInfo>>> GetCustomerIdAsync(int customerId)
        {
            var response = new BaseResponseGeneric<ICollection<OrderInfo>>();
            try
            {
                var data = await repository.GetCustomerIdAsync(customerId);
                response.Data = data;
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<OrderInfo>>> GetAsync(string? dni)
        {
            var response = new BaseResponseGeneric<ICollection<OrderInfo>>();
            try
            {
                var data = await repository.GetAsync(dni);
                response.Data = data; //mapper.Map<ICollection<OrderInfo>>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(OrderRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                await repository.CreateTransactionAsync();

                var customer = await customerRepository.GetAsync(request.CustomerId);
                if (customer is null)
                    throw new Exception(response.ErrorMessage = $"El cliente con id {request.CustomerId} no existe, favor de registralo.");
                var data = mapper.Map<Order>(request);
                var dataId = await repository.AddAsync(data);
                await repository.UpdateAsync();
                response.Success = true;
                response.Data = dataId;
            }
            catch (Exception ex)
            {
                await repository.RollBackAsync();
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
        public async Task<BaseResponse> FinalizeAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is not null)
                {
                    await repository.FinalizeAsync(id);
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
        public async Task<BaseResponse> UpdateAsync(int id, OrderRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                await repository.CreateTransactionAsync();
                var customer = await customerRepository.GetAsync(request.CustomerId);
                if (customer is null)
                    response.ErrorMessage = $"El cliente con id {request.CustomerId} no existe, favor de registralo.";
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

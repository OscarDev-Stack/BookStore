using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly ILogger<CustomerService> logger;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<BaseResponseGeneric<ICollection<CustomerResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<CustomerResponseDto>>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<CustomerResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<CustomerResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<CustomerResponseDto>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<CustomerResponseDto>>> GetAsync(string? fullName)
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => (x.FirstName + " " + x.LastName).Contains(fullName ?? string.Empty), x => x.LastName);
                response.Data = mapper.Map<ICollection<CustomerResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<CustomerResponseDto>>> GetCustomerDNIAsync(string? dni)
        {
            var response = new BaseResponseGeneric<ICollection<CustomerResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.DNI.Contains(dni ?? string.Empty), x => x.LastName);
                response.Data = mapper.Map<ICollection<CustomerResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(CustomerRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var data = mapper.Map<Customer>(request);
                var dataId = await repository.AddAsync(data);
                response.Success = true;
                response.Data = dataId;
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
        public async Task<BaseResponse> UpdateAsync(int id, CustomerRequestDto request)
        {
            var response = new BaseResponse();
            try
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
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
    }
}

using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace BookStore.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        private readonly ILogger<BookService> logger;
        private readonly IMapper mapper;

        public BookService(IBookRepository repository, ILogger<BookService> logger, IMapper mapper) 
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<BookResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }

        public Task<BaseResponseGeneric<BookResponseDto>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAuthorAsync(string? author)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetNameAsync(string? name)
        {
            throw new NotImplementedException();
        }
        public Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> FinalizeAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<BaseResponse> UpdateAsync(int id, BookRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}

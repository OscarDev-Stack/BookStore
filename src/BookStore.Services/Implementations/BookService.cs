using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Implementations;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
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
        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync(PaginationRequestDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(pagination);
                response.Data = mapper.Map<ICollection<BookResponseDto>>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<BookResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<BookResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<BookResponseDto>(data);
                response.Success = response.Data is not null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAuthorAsync(string? author, PaginationRequestDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.Author.Contains(author ?? string.Empty), x => x.Name, pagination);
                response.Data = mapper.Map<ICollection<BookResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetNameAsync(string? name, PaginationRequestDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.Name.Contains(name ?? string.Empty), x => x.Name, pagination);
                response.Data = mapper.Map<ICollection<BookResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetISBNAsync(string? ISBN, PaginationRequestDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.ISBN.Contains(ISBN ?? string.Empty), x => x.Name, pagination);
                response.Data = mapper.Map<ICollection<BookResponseDto>>(data);
                response.Success = response.Data.Count > 0;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var bookISBN = await repository.GetAsync(x => x.ISBN.Contains(request.ISBN));
                if (bookISBN is null)
                {
                    var data = mapper.Map<Book>(request);
                    var dataId = await repository.AddAsync(data);
                    response.Success = true;
                    response.Data = dataId; 
                }
                else throw new InvalidOperationException(response.ErrorMessage = $"El cliente con ISBN del libro {request.ISBN} ya existe, favor de registralo.");
            }
            catch (InvalidOperationException ex)
            {
                response.ErrorMessage = ex.Message;
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al guardar el libro.";
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
                if(data is not null)
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
        public async Task<BaseResponse> UpdateAsync(int id, BookRequestDto request)
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

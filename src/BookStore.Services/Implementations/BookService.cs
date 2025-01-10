using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Persistence.Migrations;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MusicStore.Services.Interfaces;

namespace BookStore.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        private readonly ILogger<BookService> logger;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string container = "books";

        public BookService(IBookRepository repository, ILogger<BookService> logger, IMapper mapper, IFileStorage fileStorage) 
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
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
        public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync(string? search, PaginationRequestDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                var data = await repository.GetAsync(x => x.Name.Contains(search ?? string.Empty) || 
                x.Author.Contains(search ?? string.Empty) || x.ISBN.Contains(search ?? string.Empty) || 
                x.Editorial.Contains(search ?? string.Empty), x => x.Name, pagination);
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
            Book entity = new Book();
            try
            {
                var bookISBN = await repository.GetAsync(x => x.ISBN.Contains(request.ISBN));
                
                if (bookISBN .Count == 0)
                {
                    entity = mapper.Map<Book>(request);
                    if (request.Image is not null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await request.Image.CopyToAsync(memoryStream);
                            var content = memoryStream.ToArray();
                            var extension = Path.GetExtension(request.Image.FileName);
                            entity.ImageUrl = await fileStorage.SaveFile(content, extension, container, request.Image.ContentType);
                        }
                        var dataId = await repository.AddAsync(entity);
                        response.Success = true;
                        response.Data = dataId;
                    }
                }
                else throw new InvalidOperationException(response.ErrorMessage = $"El libro con ISBN  {request.ISBN} ya existe, favor de validaro.");
            }
            catch (InvalidOperationException ex)
            {
                await fileStorage.DeleteFile(entity.ImageUrl ?? string.Empty, container);
                response.ErrorMessage = ex.Message;
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            }
            catch (Exception ex)
            {
                await fileStorage.DeleteFile(entity.ImageUrl ?? string.Empty, container);
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
                    await fileStorage.DeleteFile(data.ImageUrl ?? string.Empty, container);
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
                    if(request.Image is not null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await request.Image.CopyToAsync(memoryStream);
                            var content = memoryStream.ToArray();
                            var extension = Path.GetExtension(request.Image.FileName);
                            data.ImageUrl = await fileStorage.EditFile(content, extension, container, data.ImageUrl ?? string.Empty, request.Image.ContentType);
                        }
                    }
                    else data.ImageUrl = string.Empty;

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

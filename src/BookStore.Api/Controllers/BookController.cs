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
    public class BookController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly ILogger<BookController> logger;

        public BookController(IBookRepository repository, ILogger<BookController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            try
            {
                response.Data = await repository.GetAsync();
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
            var response = new BaseResponseGeneric<BookResponseDto>();
            try
            {
                response.Data = await repository.GetAsync(id);
                response.Success = true;
                return response.Data is not null ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrio un error al obtener la información.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(BookRequestDto book)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var bookId = await repository.AddAsync(book);
                response.Success = true;
                response.Data = bookId;
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
        public async Task<IActionResult> Put(int id, BookRequestDto book)
        {
            var response = new BaseResponse();
            try
            {
                await repository.UpdateAsync(id, book);
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

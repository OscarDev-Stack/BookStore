using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService service;

        public BookController(IBookService service) 
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await service.GetAsync();
            return response.Success ? Ok(response) : BadRequest(response);
            //var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
            //try
            //{
            //    var booksDb = await repository.GetAsync();
            //    var books = booksDb.Select(x => new BookResponseDto
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Author = x.Author,
            //        ISBN = x.ISBN,
            //        Editorial = x.Editorial,
            //        Synopsis = x.Synopsis,
            //        Status = x.Status
            //    }).ToList();
            //    response.Data = books;
            //    response.Success = true;
            //    return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = "Ocurrio un error al obtener la información.";
            //    logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
            //    return BadRequest(response);
            //}
        }
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var response = new BaseResponseGeneric<BookResponseDto>();
        //    try
        //    {
        //        var booksDb = await repository.GetAsync(id);
        //        if (booksDb is null)
        //        {
        //            logger.LogWarning($"El libro con id {id} no existe");
        //            return NotFound(response);
        //        }
        //        else
        //        {
        //            var books = new BookResponseDto
        //            {
        //                Id = booksDb.Id,
        //                Name = booksDb.Name,
        //                Author = booksDb.Author,
        //                ISBN = booksDb.ISBN,
        //                Editorial = booksDb.Editorial,
        //                Synopsis = booksDb.Synopsis,
        //                Status = booksDb.Status
        //            };

        //            response.Data = books;
        //            response.Success = true;
        //            return response.Data is not null ? Ok(response) : NotFound(response);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al obtener la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
        //[HttpGet("name")]
        //public async Task<IActionResult> GetName(string? name)
        //{
        //    var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
        //    try
        //    {
        //        var bookDb = await repository.GetAsync(x => x.Name.Contains(name ?? string.Empty), x => x.Name);

        //        var bookDto = bookDb.Select(x => new BookResponseDto
        //        {
        //            Id= x.Id,
        //            Name = x.Name,
        //            Author= x.Author,
        //            ISBN = x.ISBN,
        //            Editorial= x.Editorial,
        //            Synopsis= x.Synopsis,
        //            Status= x.Status
        //        }).ToList();

        //        response.Data = bookDto;
        //        response.Success = true;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al obtener la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
        //[HttpGet("author")]
        //public async Task<IActionResult> GetAuthor(string? author)
        //{
        //    var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();
        //    try
        //    {
        //        var bookDb = await repository.GetAsync(x => x.Author.Contains(author ?? string.Empty), x => x.Name);

        //        var bookDto = bookDb.Select(x => new BookResponseDto
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Author = x.Author,
        //            ISBN = x.ISBN,
        //            Editorial = x.Editorial,
        //            Synopsis = x.Synopsis,
        //            Status = x.Status
        //        }).ToList();

        //        response.Data = bookDto;
        //        response.Success = true;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al obtener la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> Post(BookRequestDto book)
        //{
        //    var response = new BaseResponseGeneric<int>();
        //    try
        //    {
        //        var bookDb = new Book()
        //        {
        //            Name = book.Name,
        //            Author = book.Author,
        //            ISBN = book.ISBN,
        //            Editorial = book.Editorial,
        //            Synopsis = book.Synopsis,
        //            Status = book.Status
        //        };
        //        var bookId = await repository.AddAsync(bookDb);
        //        response.Success = true;
        //        response.Data = bookId;
        //        return StatusCode((int)HttpStatusCode.Created, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al guardar la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> Put(int id, BookRequestDto book)
        //{
        //    var response = new BaseResponse();
        //    try
        //    {
        //        var bookDb = await repository.GetAsync(id);
        //        if(bookDb is null)
        //        {
        //            response.ErrorMessage = "No se encontro información.";
        //            return NotFound(response);
        //        }
        //        bookDb.Name = book.Name;
        //        bookDb.Author = book.Author;
        //        bookDb.ISBN = book.ISBN;
        //        bookDb.Editorial = book.Editorial;
        //        bookDb.Synopsis = book.Synopsis;
        //        bookDb.Status = book.Status;

        //        await repository.UpdateAsync();
        //        response.Success = true;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al actualizar la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = new BaseResponse();
        //    try
        //    {
        //        var bookDb = await repository.GetAsync(id);
        //        if (bookDb is null)
        //        {
        //            response.ErrorMessage = "No se encontro información.";
        //            return NotFound(response);
        //        }
        //        await repository.DeleteAsync(id);
        //        response.Success = true;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.ErrorMessage = "Ocurrio un error al eliminar la información.";
        //        logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        //        return BadRequest(response);
        //    }
        //}
    }
}

using BookStore.Entities;
using BookStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository repository;

        public BookController(BookRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            var data = repository.Get();
            return Ok(data);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Book> Get(int id)
        {
            var item = repository.Get(id);
            return item is not null ? Ok(item) : NotFound();
        }
        [HttpPost]
        public ActionResult<Book> Post(Book book)
        {
            repository.Add(book);
            return Ok(book);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Book book)
        {
            repository.Update(id, book);
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}

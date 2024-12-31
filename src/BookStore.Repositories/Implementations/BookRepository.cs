using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext context;

        public BookRepository(BookStoreDbContext context)
        {
            this.context = context;
        }
        public async Task<List<BookResponseDto>> GetAsync()
        {

            var items = await context.Set<Book>().AsNoTracking().ToListAsync();
            return items.Select(x => new BookResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                ISBN = x.ISBN,
                Editorial = x.Editorial,
                Synopsis = x.Synopsis,
                Status = x.Status
            }).ToList();
        }
        public async Task<BookResponseDto?> GetAsync(int id)
        {
            var item = await context.Set<Book>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var response = new BookResponseDto();
            if (item is not null)
            {
                response.Id = item.Id;
                response.Name = item.Name;
                response.Author = item.Author;
                response.ISBN = item.ISBN;
                response.Editorial = item.Editorial;
                response.Synopsis = item.Synopsis;
                response.Status = item.Status;
                return response;
            }
            else throw new InvalidOperationException($"No se encontró el registro con id {id}");
        }
        public async Task<int> AddAsync(BookRequestDto bookRequestDto)
        {
            var book = new Book()
            {
                Name = bookRequestDto.Name,
                Author = bookRequestDto.Author,
                ISBN = bookRequestDto.ISBN,
                Editorial = bookRequestDto.Editorial,
                Synopsis = bookRequestDto.Synopsis,
                Status = bookRequestDto.Status
            };
            context.Set<Book>().Add(book);
            await context.SaveChangesAsync();
            return book.Id;
        }
        public async Task UpdateAsync(int id, BookRequestDto bookRequestDto)
        {
            var item = await context.Set<Book>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (item is not null)
            {

                item.Name = bookRequestDto.Name;
                item.Author = bookRequestDto.Author;
                item.ISBN = bookRequestDto.ISBN;
                item.Editorial = bookRequestDto.Editorial;
                item.Synopsis = bookRequestDto.Synopsis;
                context.Update(item);
                await context.SaveChangesAsync();
            }
            else throw new InvalidOperationException($"No se encontró el registro con id {id}");
        }
        public async Task DeleteAsync(int id)
        {
            var item = await context.Set<Book>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (item is not null)
            {
                context.Set<Book>().Remove(item);
                await context.SaveChangesAsync();
            }
            else throw new InvalidOperationException($"No se encontró el registro con id {id}");
        }
    }
}

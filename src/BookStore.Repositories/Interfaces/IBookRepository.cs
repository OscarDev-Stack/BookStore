using BookStore.Dto.Request;
using BookStore.Dto.Response;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<int> AddAsync(BookRequestDto book);
        Task DeleteAsync(int id);
        Task<List<BookResponseDto>> GetAsync();
        Task<BookResponseDto?> GetAsync(int id);
        Task UpdateAsync(int id, BookRequestDto book);
    }
}
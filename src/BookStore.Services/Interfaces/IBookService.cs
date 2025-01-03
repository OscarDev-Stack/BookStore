using BookStore.Dto.Response;
using BookStore.Dto.Request;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<BookResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetNameAsync(string? name);
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAuthorAsync(string? author);
        Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, BookRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);

    }
}

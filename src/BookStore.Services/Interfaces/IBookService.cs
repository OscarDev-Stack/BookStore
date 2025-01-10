using BookStore.Dto.Response;
using BookStore.Dto.Request;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync(PaginationRequestDto pagination);
        Task<BaseResponseGeneric<BookResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync(string? search, PaginationRequestDto pagination);
        Task<BaseResponseGeneric<int>> AddAsync(BookRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, BookRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);

    }
}

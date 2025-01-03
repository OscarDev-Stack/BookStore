using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities.Info;

namespace BookStore.Services.Interfaces
{
    public interface IOrderBookService
    {
        Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<OrderBookInfo>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetOrderIdAsync(int id);
        Task<BaseResponseGeneric<ICollection<OrderBookResponseDto>>> GetBookIdAsync(int id);
        Task<BaseResponseGeneric<int>> AddAsync(OrderBookRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, OrderBookRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}

using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities.Info;

namespace BookStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponseGeneric<ICollection<OrderResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<OrderResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<OrderInfo>>> GetAsync(string? dni);
        Task<BaseResponseGeneric<ICollection<OrderInfo>>> GetCustomerIdAsync(int customerId);
        Task<BaseResponseGeneric<int>> AddAsync(OrderRequestDto request);
        Task<BaseResponse> UpdateAsync(int id, OrderRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse> FinalizeAsync(int id);
    }
}

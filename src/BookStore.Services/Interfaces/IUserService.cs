using BookStore.Dto.Request;
using BookStore.Dto.Response;

namespace BookStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}

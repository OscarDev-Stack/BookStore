using BookStore.Entities;
using BookStore.Entities.Info;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<ICollection<OrderInfo>> GetAsync(string? dni);
    }
}

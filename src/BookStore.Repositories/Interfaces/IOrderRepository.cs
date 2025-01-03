using BookStore.Entities;
using BookStore.Entities.Info;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<OrderInfo> GetAsync(string? dni);
        Task FinalizeAsync(int id);
        Task CreateTransactionAsync();
        Task RollBackAsync();
    }
}

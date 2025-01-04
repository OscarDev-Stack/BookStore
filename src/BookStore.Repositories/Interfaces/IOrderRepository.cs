using BookStore.Entities;
using BookStore.Entities.Info;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<ICollection<OrderInfo>> GetAsync(string? dni);
        Task<ICollection<OrderInfo>> GetCustomerIdAsync(int customerId);
        Task FinalizeAsync(int id);
        Task CreateTransactionAsync();
        Task RollBackAsync();
    }
}

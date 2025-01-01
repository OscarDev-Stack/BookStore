using BookStore.Entities;
using BookStore.Entities.Info;
namespace BookStore.Repositories.Interfaces
{
    public interface IOrderBookRepository : IRepositoryBase<OrderBook>
    {
        Task<ICollection<OrderBookInfo>> GetIdAsync(int id);
    }
}

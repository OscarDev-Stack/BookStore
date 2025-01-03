using BookStore.Entities;
using BookStore.Entities.Info;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.Repositories.Implementations
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(BookStoreDbContext context) : base(context)
        {

        }
        public override async Task<ICollection<Order>> GetAsync()
        {
            return await context.Set<Order>().Include(x => x.Customer).AsNoTracking().ToListAsync();
        }
        public override async Task<Order?> GetAsync(int id) //async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<Order>().Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<int> AddAsync(Order entity)
        {
            entity.StartDate = DateTime.Now;
            var nextNumber = await context.Set<Order>().CountAsync() + 1;
            entity.OperationNumbre = $"{nextNumber:000000}";
            await context.Set<Order>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<OrderInfo> GetAsync(string? dni)
        {
            var listBooks = await context.Set<OrderBook>().Where(x => x.Order.Customer.DNI.Contains(dni ?? string.Empty)).AsNoTracking().
                Select(x => x.Book).ToListAsync();
            var data = await context.Set<OrderBook>().Where(x => x.Order.Customer.DNI.Contains(dni ?? string.Empty)).AsNoTracking().
                Select(x => new OrderInfo
                {
                    Id = x.OrderId,
                    DateStar = x.Order.StartDate.ToShortDateString(),
                    TimeStar = x.Order.StartDate.ToShortTimeString(),
                    DateEnd = x.Order.StartDate.ToShortDateString(),
                    TimeEnd = x.Order.StartDate.ToShortTimeString(),
                    Status = x.Order.Status ? "Activo" : "Inactivo",
                    Finalized = x.Order.Finalized ? "Finalizado" : "Pendiente",
                    CustomerId = x.Order.CustomerId,
                    FullName = x.Order.Customer.FirstName + " " + x.Order.Customer.LastName,
                    DNI = x.Order.Customer.DNI,
                    Edad = x.Order.Customer.Edad,
                    Books = listBooks
                }).FirstOrDefaultAsync();
            return data ?? new OrderInfo(); ;

        }
        public async Task FinalizeAsync(int id)
        {
            var entity = await GetAsync(id);
            if(entity is not null)
            {
                entity.Finalized = true;
                entity.EndDate = DateTime.Now;
                await UpdateAsync();
            }
        }
        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }
        public async Task CreateTransactionAsync() 
        {
            await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        }
        public async Task RollBackAsync() 
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}

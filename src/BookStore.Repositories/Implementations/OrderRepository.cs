using BookStore.Entities;
using BookStore.Entities.Info;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
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
        public async Task<ICollection<OrderInfo>> GetAsync(string? dni)
        {
            var data = await context.Set<OrderBook>().Where(x => x.Order.Customer.DNI.Contains(dni ?? string.Empty)).IgnoreQueryFilters().AsNoTracking()
                .GroupBy(x => new
                {
                    x.OrderId,
                    x.Order.StartDate,
                    x.Order.Status,
                    x.Order.Finalized,
                    x.Order.OperationNumbre,
                    x.Order.CustomerId,
                    x.Order.Customer.FirstName,
                    x.Order.Customer.LastName,
                    x.Order.Customer.DNI,
                    x.Order.Customer.Edad
                })
                .Select(g => new OrderInfo
                {
                    Id = g.Key.OrderId,
                    DateStar = g.Key.StartDate.ToShortDateString(),
                    TimeStar = g.Key.StartDate.ToShortTimeString(),
                    DateEnd = g.Key.StartDate.ToShortDateString(),
                    TimeEnd = g.Key.StartDate.ToShortTimeString(),
                    Status = g.Key.Status ? "Activo" : "Inactivo",
                    Finalized = g.Key.Finalized ? "Finalizado" : "Pendiente",
                    OperationNumbre = g.Key.OperationNumbre,
                    CustomerId = g.Key.CustomerId,
                    FullName = g.Key.FirstName + " " + g.Key.LastName,
                    DNI = g.Key.DNI,
                    Edad = g.Key.Edad,
                    Books = g.Select(x => x.Book).ToList()
                }).ToListAsync();
            return data;
        }
        public async Task<ICollection<OrderInfo>> GetCustomerIdAsync(int customerId)
        {
            //return await context.Set<Order>().Include(x => x.Customer).Where(x => x.CustomerId == customerId).ToListAsync();
            var data = await context.Set<OrderBook>().Where(x => x.Order.CustomerId == customerId).IgnoreQueryFilters().AsNoTracking()
                .GroupBy(x => new
                {
                    x.OrderId,
                    x.Order.StartDate,
                    x.Order.Status,
                    x.Order.Finalized,
                    x.Order.OperationNumbre,
                    x.Order.CustomerId,
                    x.Order.Customer.FirstName,
                    x.Order.Customer.LastName,
                    x.Order.Customer.DNI,
                    x.Order.Customer.Edad
                })
                .Select(g => new OrderInfo
                {
                    Id = g.Key.OrderId,
                    DateStar = g.Key.StartDate.ToShortDateString(),
                    TimeStar = g.Key.StartDate.ToShortTimeString(),
                    DateEnd = g.Key.StartDate.ToShortDateString(),
                    TimeEnd = g.Key.StartDate.ToShortTimeString(),
                    Status = g.Key.Status ? "Activo" : "Inactivo",
                    Finalized = g.Key.Finalized ? "Finalizado" : "Pendiente",
                    OperationNumbre = g.Key.OperationNumbre,
                    CustomerId = g.Key.CustomerId,
                    FullName = g.Key.FirstName + " " + g.Key.LastName,
                    DNI = g.Key.DNI,
                    Edad = g.Key.Edad,
                    Books = g.Select(x => x.Book).ToList()
                }).ToListAsync();
            return data;
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

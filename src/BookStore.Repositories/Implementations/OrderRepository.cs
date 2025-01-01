using BookStore.Entities;
using BookStore.Entities.Info;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ICollection<OrderInfo>> GetAsync(string? dni)
        {
            return await context.Set<Order>().Where(x => x.Customer.DNI.Contains(dni ?? string.Empty)).
                AsNoTracking().Select(x => new OrderInfo
                {
                    Id = x.Id,
                    DateStar = x.StartDate.ToShortDateString(),
                    TimeStar = x.StartDate.ToShortTimeString(),
                    Status = x.Status ? "Activo" : "Inactivo",
                    CustomerId = x.CustomerId,
                    FullName = $"{x.Customer.FirstName} {x.Customer.LastName}",
                    DNI = x.Customer.DNI,
                    Edad = x.Customer.Edad
                }).ToListAsync();
        }
    }
}

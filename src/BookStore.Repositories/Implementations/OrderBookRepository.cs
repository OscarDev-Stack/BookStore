using BookStore.Entities;
using BookStore.Entities.Info;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Implementations
{
    public class OrderBookRepository : RepositoryBase<OrderBook>, IOrderBookRepository
    {
        public OrderBookRepository(BookStoreDbContext context) : base(context)
        {
        }
        public override async Task<ICollection<OrderBook>> GetAsync()
        {
            return await context.Set<OrderBook>().AsNoTracking().ToListAsync();
        }
        public async Task<ICollection<OrderBookInfo>> GetIdAsync(int id)
        {
            return await context.Set<OrderBook>()
                .Where(x => x.Id == id).AsNoTracking()
                .Select(x => new OrderBookInfo
                {
                    Id = x.Id,
                    BookId = x.BookId,
                    BookName = x.Book.Name,
                    BookAuthor = x.Book.Author,
                    BookISBN = x.Book.ISBN,
                    BookEditorial = x.Book.Editorial,
                    BookSynopsis = x.Book.Synopsis,
                    ImageUrl = x.Book.ImageUrl,
                    BookStatus = x.Book.Status ? "Activo" : "Inactivo",
                    OrderId = x.OrderId,
                    OrderDateStar = x.Order.StartDate.ToShortDateString(),
                    OrderTimeStar = x.Order.StartDate.ToShortTimeString(),
                    OrderStatus = x.Order.Status ? "Activo" : "Inactivo",
                    CustomerId = x.Order.CustomerId,
                    CustomerFullName = $"{x.Order.Customer.FirstName} {x.Order.Customer.LastName}",
                    CustomerDNI = x.Order.Customer.DNI,
                    CustomerEdad = x.Order.Customer.Edad
                }).ToListAsync();

        }
    }
}

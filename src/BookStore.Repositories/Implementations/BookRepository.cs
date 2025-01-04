using BookStore.Dto.Request;
using BookStore.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using BookStore.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Repositories.Implementations
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private readonly IHttpContextAccessor httpContext;

        public BookRepository(BookStoreDbContext context, IHttpContextAccessor httpContext) : base(context)
        {
            this.httpContext = httpContext;
        }
        public async Task<ICollection<Book>> GetAsync(PaginationRequestDto pagination)
        {
            var queryable = context.Set<Book>().AsNoTracking().AsQueryable();
            await httpContext.HttpContext.InsertPaginationHeader(queryable);
            var response = await queryable.OrderBy(x => x.Id).Paginate(pagination).ToListAsync();
            return response;
        }
        public async Task<ICollection<Book>> GetAsync<TKey>(Expression<Func<Book, bool>> predicate, Expression<Func<Book, TKey>> orderBy, PaginationRequestDto pagination)
        {
            var queryable = context.Set<Book>().Where(predicate).OrderBy(orderBy).AsNoTracking().AsQueryable();
            await httpContext.HttpContext.InsertPaginationHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();
            return response;
        }

    }
}

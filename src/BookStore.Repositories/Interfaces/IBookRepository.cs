using BookStore.Dto.Request;
using BookStore.Entities;
using System.Linq.Expressions;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        Task<ICollection<Book>> GetAsync(PaginationRequestDto pagination);
        Task<ICollection<Book>> GetAsync<TKey>(Expression<Func<Book, bool>> predicate, Expression<Func<Book, TKey>> orderBy, PaginationRequestDto pagination);
    }
}
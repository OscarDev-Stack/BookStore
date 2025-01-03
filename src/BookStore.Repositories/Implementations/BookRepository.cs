using BookStore.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories.Implementations
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context) : base(context)
        {
        }
        
    }
}

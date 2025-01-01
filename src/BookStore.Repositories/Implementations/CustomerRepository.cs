using BookStore.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories.Implementations
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(BookStoreDbContext context) : base(context)
        {
        }
    }
}

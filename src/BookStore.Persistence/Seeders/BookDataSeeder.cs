using Microsoft.Extensions.DependencyInjection;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence.Seeders
{
    public class BookDataSeeder
    {
        private readonly IServiceProvider _serviceProvider;

        public BookDataSeeder(IServiceProvider service)
        {
            _serviceProvider = service;
        }
        public async Task SeedAsync()
        {
            using (var context = _serviceProvider.GetRequiredService<BookStoreDbContext>())
            {
                var lstBooks = new List<Book>
                {
                    new Book
                    {
                        Name = "Constitución Política de los Estados Unidos Mexicanos",
                        Author = "Ediciones Fiscales ISEF",
                        ISBN = "9786075415406",
                        Editorial = "Ediciones Fiscales ISEF",
                        Synopsis = "Constitución Política de losEstados Unidos Mexicanos\r\n\r\nVersión del 17 octubre con Reformaal Poder Judicial, a la Guardia Nacional y a los Pueblos Indígenas impresas"
                    }
                };
                var booksNamesToAdd = lstBooks.Select(x => x.ISBN).ToHashSet();
                var existingBookISBN = await context.Set<Book>().Where(w => booksNamesToAdd.Contains(w.ISBN)).Select(s => s.ISBN).ToListAsync();
                var booksToAdd = lstBooks.Where(w => !existingBookISBN.Contains(w.ISBN)).ToList();
                if (booksToAdd.Any())
                {
                    await context.Set<Book>().AddRangeAsync(booksToAdd);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

using BookStore.Entities;
using BookStore.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookStore.Persistence
{
    public class BookStoreDbContext : IdentityDbContext<BookStoreUserIdentity>
    {
        public BookStoreDbContext(DbContextOptions options) : base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<OrderInfo>();
            modelBuilder.Ignore<OrderBookInfo>();
            modelBuilder.Ignore<AppSettings>();
            modelBuilder.Entity<BookStoreUserIdentity>(x => x.ToTable("User"));
            modelBuilder.Entity<IdentityRole>(x => x.ToTable("Role"));
            modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRole"));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}

using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations
{
    public class OrderBookConfiguration : IEntityTypeConfiguration<OrderBook>
    {
        public void Configure(EntityTypeBuilder<OrderBook> builder)
        {
            builder.ToTable("OrderBooks", "BookStore");
            builder.Property(x => x.Status).HasDefaultValueSql("1");
            builder.HasQueryFilter(x => x.Status);
        }
    }
}

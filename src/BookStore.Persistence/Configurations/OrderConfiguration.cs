using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.StartDate).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.OperationNumbre).HasMaxLength(10);
            builder.Property(x => x.Status).HasDefaultValueSql("1");
            builder.ToTable("Orders", "BookStore");
            builder.HasQueryFilter(x => x.Status);
        }
    }
}

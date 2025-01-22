using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.ImageUrl).HasMaxLength(100).IsUnicode(false);
            builder.HasIndex(x => x.Name);
            builder.ToTable("Books", "BookStore");
            builder.Property(x => x.Status).HasDefaultValueSql("1");
            builder.HasQueryFilter(x => x.Status);
        }
    }
}

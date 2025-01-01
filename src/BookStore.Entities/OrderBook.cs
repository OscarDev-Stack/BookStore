namespace BookStore.Entities
{
    public class OrderBook : EntityBase
    {
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; } = default!;
        public virtual Order Order { get; set; } = default!;
    }
}

namespace BookStore.Entities
{
    public class Order : EntityBase
    {
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;
    }
}

namespace BookStore.Entities
{
    public class Order : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Finalized { get; set; }
        public int CustomerId { get; set; }
        public string OperationNumbre { get; set; } = default!;
        public virtual Customer Customer { get; set; } = default!;
    }
}

namespace BookStore.Entities
{
    public class Order : EntityBase
    {
        public DateTime StartDate { get; set; }
        public List<Book> Books { get; set; } = default!;
    }
}

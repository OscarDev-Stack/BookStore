namespace BookStore.Entities.Info
{
    public class OrderBookInfo
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; } = default!;
        public string BookAuthor { get; set; } = default!;
        public string BookISBN { get; set; } = default!;
        public string BookEditorial { get; set; } = default!;
        public string BookSynopsis { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string BookStatus { get; set; } = default!;
        public int OrderId { get; set; }
        public string OrderDateStar { get; set; } = default!;
        public string OrderTimeStar { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; } = default!;
        public string CustomerDNI { get; set; } = default!;
        public int CustomerEdad { get; set; }
    }
}

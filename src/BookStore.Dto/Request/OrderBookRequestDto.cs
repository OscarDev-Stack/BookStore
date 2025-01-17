namespace BookStore.Dto.Request
{
    public class OrderBookRequestDto
    {
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public bool Status { get; set; } = true;
    }
}

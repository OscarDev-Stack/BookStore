using BookStore.Entities;

namespace BookStore.Dto.Response
{
    public class OrderBookResponseDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; } = default!;
        public Order Order { get; set; } = default!;
        public bool Status { get; set; } = true;
    }
}

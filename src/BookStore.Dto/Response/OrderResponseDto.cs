

using BookStore.Entities;

namespace BookStore.Dto.Response
{
    public class OrderResponseDto
    {
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public bool Status { get; set; }
    }
}

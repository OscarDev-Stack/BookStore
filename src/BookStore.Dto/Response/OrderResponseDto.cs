

using BookStore.Entities;

namespace BookStore.Dto.Response
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Finalized { get; set; }
        public int CustomerId { get; set; }
        public string OperationNumbre { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public bool Status { get; set; }
    }
}

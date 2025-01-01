namespace BookStore.Dto.Request
{
    public class OrderRequestDto
    {
        public DateTime StartDate { get; set; }
        public int CustomerId { get; set; }
        public bool Status { get; set; }
    }
}

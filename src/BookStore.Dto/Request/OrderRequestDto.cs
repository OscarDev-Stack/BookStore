namespace BookStore.Dto.Request
{
    public class OrderRequestDto
    {
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public bool Finalized { get; set; }
        public int CustomerId { get; set; }
        public bool Status { get; set; } = true;
    }
}

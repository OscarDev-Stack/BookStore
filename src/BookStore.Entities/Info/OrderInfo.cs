namespace BookStore.Entities.Info
{
    public class OrderInfo
    {
        public int Id { get; set; } 
        //public DateTime StartDate { get; set; }
        public string DateStar { get; set; } = default!;
        public string TimeStar { get; set; } = default!;
        public string Status { get; set; } = default!;
        public int CustomerId { get; set; }
        public string FullName { get; set; } = default!;
        public string DNI { get; set; } = default!;
        public int Edad { get; set; }
    }
}

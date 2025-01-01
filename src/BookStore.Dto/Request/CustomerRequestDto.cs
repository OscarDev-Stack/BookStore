namespace BookStore.Dto.Request
{
    public class CustomerRequestDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string DNI { get; set; } = default!;
        public int Edad { get; set; }
        public bool Status { get; set; }
    }
}

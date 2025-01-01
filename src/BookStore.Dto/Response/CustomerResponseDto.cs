namespace BookStore.Dto.Response
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string DNI { get; set; } = default!;
        public int Edad { get; set; }
        public bool Status { get; set; }
    }
}

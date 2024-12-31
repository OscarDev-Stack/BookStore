namespace BookStore.Dto.Request
{
    public class BookRequestDto
    {
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public string Editorial { get; set; } = default!;
        public string Synopsis { get; set; } = default!;
        public bool Status { get; set; } = true;
    }
}

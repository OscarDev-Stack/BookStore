namespace BookStore.Dto.Response
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public string Editorial { get; set; } = default!;
        public string Synopsis { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public bool Status { get; set; } = true;
    }
}

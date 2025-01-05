using BookStore.Dto.Validations;
using Microsoft.AspNetCore.Http;
using MusicStore.Dto.Validations;

namespace BookStore.Dto.Request
{
    public class BookRequestDto
    {
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public string Editorial { get; set; } = default!;
        public string Synopsis { get; set; } = default!;
        [FileSizeValidation(1)]
        [FileTypeValidation(FileTypeGroup.Image)]
        public IFormFile? Image { get; set; }
        public bool Status { get; set; } = true;
    }
}

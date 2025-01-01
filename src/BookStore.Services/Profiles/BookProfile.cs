using AutoMapper;
using BookStore.Dto.Response;
using BookStore.Entities;

namespace BookStore.Services.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookResponseDto>();
        }
    }
}

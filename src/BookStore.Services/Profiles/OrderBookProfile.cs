using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Entities.Info;

namespace BookStore.Services.Profiles
{
    public class OrderBookProfile : Profile
    {
        public OrderBookProfile()
        {
            CreateMap<OrderBook, OrderBookInfo>()
                .ForMember(d => d.BookId, o => o.MapFrom(x => x.BookId))
                .ForMember(d => d.BookName, o => o.MapFrom(x => x.Book.Name))
                .ForMember(d => d.BookAuthor, o => o.MapFrom(x => x.Book.Author))
                .ForMember(d => d.BookISBN, o => o.MapFrom(x => x.Book.ISBN))
                .ForMember(d => d.BookEditorial, o => o.MapFrom(x => x.Book.Editorial))
                .ForMember(d => d.BookSynopsis, o => o.MapFrom(x => x.Book.Synopsis))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.Book.ImageUrl))
                .ForMember(d => d.BookStatus, o => o.MapFrom(x => x.Book.Status ? "Activo" : "Inactivo"))
                .ForMember(d => d.OrderId, o => o.MapFrom(x => x.OrderId))
                .ForMember(d => d.OrderDateStar, o => o.MapFrom(x => x.Order.StartDate.ToShortDateString()))
                .ForMember(d => d.OrderTimeStar, o => o.MapFrom(x => x.Order.StartDate.ToShortTimeString()))
                .ForMember(d => d.OrderDateEnd, o => o.MapFrom(x => x.Order.EndDate.ToShortDateString()))
                .ForMember(d => d.OrderTimeEnd, o => o.MapFrom(x => x.Order.EndDate.ToShortTimeString()))
                .ForMember(d => d.OrderStatus, o => o.MapFrom(x => x.Order.Status ? "Activo" : "Inactivo"))
                .ForMember(d => d.OrderFinalized, o => o.MapFrom(x => x.Order.Finalized ? "Finalizado" : "Pendiente"))
                .ForMember(d => d.CustomerId, o => o.MapFrom(x => x.Order.CustomerId))
                .ForMember(d => d.CustomerFullName, o => o.MapFrom(x => x.Order.Customer.FirstName + " " + x.Order.Customer.LastName))
                .ForMember(d => d.CustomerDNI, o => o.MapFrom(x => x.Order.Customer.DNI))
                .ForMember(d => d.CustomerEdad, o => o.MapFrom(x => x.Order.Customer.Edad));
            CreateMap<OrderBook, OrderBookResponseDto>();
            CreateMap<OrderBookRequestDto, OrderBook>();
        }
    }
}

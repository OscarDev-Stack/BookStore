using AutoMapper;
using BookStore.Dto.Request;
using BookStore.Dto.Response;
using BookStore.Entities;
using BookStore.Entities.Info;

namespace BookStore.Services.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderInfo>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderRequestDto, Order>(); 
        }
    }
}

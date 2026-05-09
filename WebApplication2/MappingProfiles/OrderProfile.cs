using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.DTOs.OrderDetails;

namespace CRM.WebApp.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Order Mappings (Add these)
            CreateMap<Order, OrderDto>()
                //.ForMember(item => item.TotalAmount, opt => opt.MapFrom(z => z.OrderDetails.Sum(m => m.Product.TotalCost)))
                .ReverseMap();


            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>()
                //.ForMember(item => item.TotalAmount, opt => opt.MapFrom(z => z.OrderDetails.Sum(m => m.Product.TotalCost)))
                .ReverseMap();


            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

            CreateMap<OrderDetailCreateDto, OrderDetails>().ReverseMap();
            CreateMap<OrderDetailUpdateDto, OrderDetails>().ReverseMap();
        }
    }
}

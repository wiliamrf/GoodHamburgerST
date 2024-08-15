using AutoMapper;
using GoodHamburger.Business.Models;

namespace GoodHamburger.API.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProductDTO,Product>().ReverseMap();
            CreateMap<OrderDTO,Order>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();


        }
    }
}

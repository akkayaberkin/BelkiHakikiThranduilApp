using AutoMapper;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;

namespace BelkiHakiki.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductSaveDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<AppUsers, UserRegisterDto>().ReverseMap();
            CreateMap<AppUsers, UserSignInDto>().ReverseMap();
            CreateMap<AppUsers, UserDto>().ReverseMap();
            CreateMap<AppUsers, UserUpdateDto>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderDto>().ReverseMap();
            CreateMap<CustomerOrder, CustomerOrderUpdateDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();

        }
    }
}

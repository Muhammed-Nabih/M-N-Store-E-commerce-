using AutoMapper;
using M_N_Store.Domain.Dtos;
using M_N_Store.Domain.Entities;

namespace M_N_Store.Mapping_Profiles
{
    public class MappingBasket : Profile
    {
        public MappingBasket()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}

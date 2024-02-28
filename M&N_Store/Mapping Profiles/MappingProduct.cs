using AutoMapper;
using M_N_Store.Core.Dtos;
using M_N_Store.Helper;
using N_Store.Domain.Entities;

namespace M_N_Store.Mapping_Profiles
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();

        }
    }
}

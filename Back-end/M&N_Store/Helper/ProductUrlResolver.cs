using AutoMapper;
using M_N_Store.Core.Dtos;
using N_Store.Domain.Entities;

namespace M_N_Store.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductPicture))
            {
                return _config["ApiURL"] + source.ProductPicture;
            }
            return null;
        }
    }


}

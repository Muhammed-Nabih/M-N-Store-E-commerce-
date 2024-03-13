using Microsoft.AspNetCore.Http;
using N_Store.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace M_N_Store.Core.Dtos
{

    public class BaseProduct
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,9999,ErrorMessage = "Price Limited by {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]+",ErrorMessage ="{0} Must Be Number !")]
        public decimal Price { get; set; }
    }

    public class ProductDto : BaseProduct
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string ProductPicture { get; set; }
    }

    public class ReturnProductDto
    {
        public int TotalItems { get; set; }
        public List<ProductDto> ProductDtos { get; set; }
    }

    public class CreateProductDto : BaseProduct
    {

        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }
    public class UpdateProductDto : BaseProduct
    {

        public int CategoryId { get; set; }
        public string OldImage { get; set; }
        public IFormFile Image { get; set; }
    }
}

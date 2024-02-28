using System.ComponentModel.DataAnnotations;

namespace M_N_Store.Core.Dtos
{
  
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ListingCategoryDto : CategoryDto
    {
        public int Id { get; set; }
    }

    public class UpdateCategoryDto : CategoryDto
    {
        public int Id { get; set; }
    }


}

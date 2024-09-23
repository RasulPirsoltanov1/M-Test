using System.ComponentModel.DataAnnotations;

namespace Categories.WebAPI
{
    public class CreateCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is mandatory.")]
        [MaxLength(60, ErrorMessage = "Max 60 characters.")]
        public string? Name { get; set; }
    }
}

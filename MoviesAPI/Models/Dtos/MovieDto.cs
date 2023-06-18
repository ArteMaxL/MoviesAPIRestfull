using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos
{
    public class MovieDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is mandatory.")]
        [MaxLength(60, ErrorMessage = "Max 60 characters.")]
        public string Name { get; set; }
        public string ImageAdress { get; set; }

        [Required(ErrorMessage = "The description is mandatory.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The duration is mandatory.")]
        public int Duration { get; set; }
        public enum ClassificationType
        {
            Seven, Thirteen, Sixteen, Eighteen
        }
        public ClassificationType Classification { get; set; }
        public DateTime InitialDate { get; set; }
        public int CategoryId { get; set; }
    }
}

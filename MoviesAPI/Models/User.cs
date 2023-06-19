using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string?UserName { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }
    }
}

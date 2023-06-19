using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "The name is mandatory.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The username is mandatory.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "The password is mandatory.")]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "The username is mandatory.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "The password is mandatory.")]
        public string? Password { get; set; }
    }
}

using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;

namespace MoviesAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool IsUniqueUserName(string name);
        Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
        Task<User> Register(UserRegisterDto userRegisterDto);
    }
}

using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Utils;
using XSystem.Security.Cryptography;

namespace MoviesAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _bd;

        public UserRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public User GetUser(int id)
        {
            return _bd.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _bd.Users.OrderBy(u => u.UserName).ToList();
        }

        public bool IsUniqueUserName(string name)
        {
            return _bd.Users.FirstOrDefault(u => u.UserName == name) == null;
        }       

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            var encryptedPassword = Encrypted.GetMd5(userRegisterDto.Password);

            User user = new User 
            { 
                UserName = userRegisterDto.UserName, 
                Password = encryptedPassword,
                Name = userRegisterDto.Name,
                Role = userRegisterDto.Role
            };

            _bd.Users.Add(user);
            await _bd.SaveChangesAsync();
            user.Password = encryptedPassword;

            return user;
        }

        public Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }        
    }
}

using Microsoft.IdentityModel.Tokens;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository.IRepository;
using MoviesAPI.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace MoviesAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _bd;
        private string secretKey;

        public UserRepository(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            secretKey = config.GetValue<string>("ApiSettings:Secret");
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

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var encryptedPassword = Encrypted.GetMd5(userLoginDto.Password);

            var user = _bd.Users.FirstOrDefault(
                        u => u.UserName.ToLower() == userLoginDto.UserName.ToLower() &&
                        u.Password == encryptedPassword);

            if (user == null) 
            { 
                return new UserLoginResponseDto()
                {
                    Token = string.Empty,
                    User = null
                }; 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return userLoginResponseDto;
        }        
    }
}

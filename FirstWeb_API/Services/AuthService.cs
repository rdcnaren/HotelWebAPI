using AutoMapper;
using FirstWeb_API.Data;
using FirstWeb_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalHotel.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstWeb_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _UserDB;
        private readonly IConfiguration _Configuration;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext userDB, IConfiguration configuration ,IMapper mapper)
        {
            _UserDB = userDB;
            _Configuration = configuration;
            _mapper = mapper;
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _UserDB.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _UserDB.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginRequestDTO.Email.ToLower() && u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                return null;
            }
             var token = GenerateJwtToken(user);
            // Generate JWT Token
            return new LoginResponseDTO
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            };
        }

        public async Task<UserDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                if(await IsEmailExistsAsync(registrationRequestDTO.Email))
                {
                    return null;
                }
                User user = new()
                {
                    Email = registrationRequestDTO.Email,
                    Name = registrationRequestDTO.Name,
                    Password = registrationRequestDTO.Password,
                    CreatedAt = DateTime.Now,
                };

                await _UserDB.Users.AddAsync(user);
                await _UserDB.SaveChangesAsync();

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An expected error occured during user registration", ex);
            }
        }

        private string GenerateJwtToken(User user)
        {
            // Implement JWT token generation logic here
            var Key = Encoding.ASCII.GetBytes(_Configuration.GetSection("JwtToken")["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tasks.BLL.DTOs;
using Tasks.BLL.Models;
using Tasks.DAL.EF;
using Tasks.DAL.Repositories;

namespace Tasks.BLL.Services
{
    public interface IUserService
    {
        Task<string> Login(AuthDTO model);
    }

    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
                           IDateTimeProvider dateTimeProvider,
                           JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> Login(AuthDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var existingUser = await _userRepository.FindByEmail(model.Email);

            if (existingUser != null)
            {
                if (await _userRepository.CheckPassword(existingUser, model.Password))
                {
                    return GenerateJwtToken(existingUser);
                }

                return "Invalid password";
            }

            return "Invalid login";
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("email", user.Email),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = _dateTimeProvider.GetCurrentUTC.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}

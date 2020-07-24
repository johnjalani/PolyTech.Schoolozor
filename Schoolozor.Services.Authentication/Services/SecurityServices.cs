using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Schoolozor.Model;
using Schoolozor.Shared.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Schoolozor.Services.Authentication.Services
{
    public class SecurityServices
    {
        private readonly UserManager<SchoolUser> _user;
        private readonly IConfiguration _configuration;
        public SecurityServices(
            UserManager<SchoolUser> user,
            IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }
        public LoginResult GetLoginResult(SchoolUser user)
        {
            DateTime willExpireOn = DateTime.UtcNow.AddMinutes(5);
            var result = new LoginResult()
            {
                UserId = user.Id,
                ExpirationDate = willExpireOn,
                Token = GenerateJwtToken(user.Id, willExpireOn)
            };

            return result;
        }
        public SchoolUser GetUser(string Id)
        {
            var userId = _user.Users.FirstOrDefault(x => x.Id == Id);
            if (userId != null)
            {
                return userId;
            }
            return null;
        }
        public IEnumerable<SchoolUser> GetUsers()
        {
            return _user.Users.ToList();
        }
        private string GenerateJwtToken(string userId, DateTime expry)
        {
            try
            {

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_configuration["Jwt:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _configuration["Jwt:Audience"],
                    Issuer = _configuration["Jwt:Issuer"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = expry,
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return string.Empty;
            }
        }
    }
}

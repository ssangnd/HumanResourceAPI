using Entities.DTOs;
using Entities.Models;
using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HumanResource.Utility
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User _user;

        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> Createtoken()
        {
            var signingCredentials = GetSingingCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSingingCredentials()
        {
            var secretKey = _configuration.GetSection("JwtSettings:secretKey").Value;
            var key = Encoding.UTF8.GetBytes(secretKey);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim> { 
                new Claim(
                    ClaimTypes.Email,
                    ClaimTypes.Name,
                    _user.UserName,
                    ClaimTypes.Expiration
                )
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var expireValue = int.Parse(jwtSettings.GetSection("expires").Value);
            var expires = DateTime.Now.AddMinutes(expireValue);
            var tokenOptions = new JwtSecurityToken(
                jwtSettings.GetSection("validIssuer").Value,
                jwtSettings.GetSection("validAudience").Value,
                claims,
                expires: expires,
                signingCredentials:  signingCredentials
             );

            return tokenOptions;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationFto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);
            var check= await _userManager.CheckPasswordAsync(_user, userForAuth.Password); ;
            var result = _user != null && check;
            return (result);
        }
    }
}

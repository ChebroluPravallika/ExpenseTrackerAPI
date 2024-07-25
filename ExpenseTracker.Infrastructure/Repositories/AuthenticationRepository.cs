using ExpenseTracker.Infrastructure.Interfaces;
using ExpenseTracker.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Infrastructure.Repositories
{
    public class AuthenticationRepository: IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;


        public AuthenticationRepository(UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<AuthToken> Login([FromBody] User model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim("UserId",user.UserName),
                };
                var token = GetToken(authClaims);
                AuthToken result = new AuthToken();
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                result.Expiry = token.ValidTo;
                Console.WriteLine(result);
                return result;
            }
            return new AuthToken();
        }

        public async Task<Response> Register([FromBody] User model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new Response { Status = "Error", Message = "User already exists!" };

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
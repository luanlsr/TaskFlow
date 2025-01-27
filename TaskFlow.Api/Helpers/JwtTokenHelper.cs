using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Api.Helpers
{
    public static class JwtTokenHelper
    {
        /// <summary>
        /// Gera um token JWT para o usuário, incluindo claims de role.
        /// </summary>
        /// <param name="user">A instância de ApplicationUser</param>
        /// <param name="userManager">UserManager para recuperar roles etc.</param>
        /// <param name="configuration">Para ler secretKey, issuer, audience</param>
        public static async Task<string> GenerateJwtToken(
            ApplicationUser user,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            // Lê configs "JwtSettings" (ou "JwtBearerTokenSettings") do appsettings.json
            var secretKey = configuration["JwtSettings:SecretKey"];
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Claims básicas
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
            };

            // Claims de roles (se houver)
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

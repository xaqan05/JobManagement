using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace JobManagement.API.Services;
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(AppUser user)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "JobManagementSecretKeyForDevelopmentOnly12345";
        var issuer = _configuration["Jwt:Issuer"] ?? "JobManagement";
        var audience = _configuration["Jwt:Audience"] ?? "JobManagementUsers";

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}".Trim()),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
            new Claim("userType", user.UserType.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Escalas.Infrastructure.Authentication;
public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;
    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public UsuarioTokenModel Generate(Usuario user)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
        };

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenConfiguration:Key"] ?? string.Empty)),
            SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(5);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new UsuarioTokenModel()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Token created."
        };
    }

    public bool ValidateToken(string token)
    {
        if(!IsToken(token))
            return false;

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var expDate = jwtToken.ValidTo;
         
        return expDate > DateTime.UtcNow;
    }

    private static bool IsToken(string token)
    {
        var parts = token?.Split('.');
        if (parts?.Length != 3)
        {
            return false;
        }

        return IsBase64Url(parts[0]) && IsBase64Url(parts[1]) && IsBase64Url(parts[2]);
    }

   private static bool IsBase64Url(string input)
    {
        // Remover caracteres de preenchimento '=' caso existam
        input = input.TrimEnd('=');

        // Verifica se todos os caracteres da string são válidos para Base64Url (A-Z, a-z, 0-9, '-', '_')
        string base64UrlPattern = @"^[A-Za-z0-9_-]*$";
        return Regex.IsMatch(input, base64UrlPattern);
    }
}

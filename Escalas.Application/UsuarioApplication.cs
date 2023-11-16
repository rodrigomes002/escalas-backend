using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Escalas.Application;

public class UsuarioApplication : IUsuarioApplication
{ 
   const int keySize = 64;
   const int iterations = 350000;
   HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    private readonly IConfiguration _configuration;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioApplication(IConfiguration configuration, IUsuarioRepository usuarioRepository)
    {
        _configuration = configuration;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<int>> CadastrarAsync(Usuario usuario)
    {
        Log.Information("Cadastrando usuario");
        
        var hash = HashPasword(usuario.Password, out var salt);
        usuario.PasswordHash = hash;
        usuario.PasswordSalt = salt;
        
        var result = await _usuarioRepository.CadastrarAsync(usuario);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar um usuário");

        return Result<int>.Ok(result);
    }

    public async Task<Result<UsuarioTokenModel>> LoginAsync(Usuario usuario)
    {
        Log.Information("Cadastrando usuario");
        var result = await _usuarioRepository.GetUsuarioByUsernameAsync(usuario.Username);

        if (result is null)
            return Result<UsuarioTokenModel>.NotFoundResult();
        
        if (!VerifyPassword(usuario.Password, result.PasswordHash, result.PasswordSalt))
            return Result<UsuarioTokenModel>.Error("Senha inválida");
        
        return Result<UsuarioTokenModel>.Ok(await GeraToken(result));
    }

   private string HashPasword(string password, out byte[] salt)
   {
        salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return Convert.ToHexString(hash);
    }

   private bool VerifyPassword(string password, string hash, byte[] salt)
   {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
   } 

   public async Task<UsuarioTokenModel> GeraToken(Usuario user)
   {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var jwtAudience = env == "Production" ? Environment.GetEnvironmentVariable("Audience") : _configuration["TokenConfiguration:Audience"];
        var jwtIssuer = env == "Production" ? Environment.GetEnvironmentVariable("Issuer") : _configuration["TokenConfiguration:Issuer"];
        var jwtKey = env == "Production" ? Environment.GetEnvironmentVariable("JwtKey") : _configuration["Jwt:Key"];
        var jwtExpires = env == "Production" ? Environment.GetEnvironmentVariable("ExpireHours") : _configuration["TokenConfiguration:ExpireHours"];

        //var roles = await _userManager.GetRolesAsync(user);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim("role", roles[0])
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(double.Parse(jwtExpires));

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
            );

        return new UsuarioTokenModel()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Token gerado."
        };
    }
}
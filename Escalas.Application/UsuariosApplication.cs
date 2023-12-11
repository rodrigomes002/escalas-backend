using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Security.Cryptography;
using System.Text;

namespace Escalas.Application;

public class UsuariosApplication : IUsuariosApplication
{
    const int keySize = 64;
    const int iterations = 350000;
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    private readonly IConfiguration _configuration;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuariosApplication(IConfiguration configuration, IJwtProvider jwtProvider, IUsuarioRepository usuarioRepository)
    {
        _configuration = configuration;
        _jwtProvider = jwtProvider;
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

        return Result<UsuarioTokenModel>.Ok(_jwtProvider.Generate(result));
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
}
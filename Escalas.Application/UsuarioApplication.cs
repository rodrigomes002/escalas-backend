using System.Security.Cryptography;
using System.Text;
using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Serilog;

namespace Escalas.Application;

public class UsuarioApplication : IUsuarioApplication
{ 
   const int keySize = 64;
   const int iterations = 350000;
   HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioApplication(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<int>> CadastrarUsuarioAsync(Usuario usuario)
    {
        Log.Information("Cadastrando usuario");
        
        var hash = HashPasword(usuario.Password, out var salt);
        usuario.PasswordHash = hash;
        usuario.PasswordSalt = salt;
        
        var result = await _usuarioRepository.CadastrarUsuarioAsync(usuario);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar um usuário");

        return Result<int>.Ok(result);
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
    {
        Log.Information("Buscando usuarios");
        return await _usuarioRepository.GetUsuariosAsync();
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
using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Escalas.Application.Services;

public class UsuarioService : IUsuariosService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICryptographyProvider _cryptographyProvider;
    private readonly IMemoryCache _memoryCache;
    public UsuarioService(IJwtProvider jwtProvider, IUsuarioRepository usuarioRepository, ICryptographyProvider cryptographyProvider, IMemoryCache memoryCache)
    {
        _jwtProvider = jwtProvider;
        _usuarioRepository = usuarioRepository;
        _cryptographyProvider = cryptographyProvider;
        _memoryCache = memoryCache;
    }

    public async Task<Result<int>> CadastrarAsync(Usuario usuario)
    {
        var user = await _usuarioRepository.GetUsuarioByUsernameAsync(usuario.Username);

        if (user is not null)
            return Result<int>.Error("O usuário informado já existe");

        Log.Information("Cadastrando usuario");

        var auth = _cryptographyProvider.HashPasword(usuario.Password);
        usuario.PasswordHash = auth.Hash;
        usuario.PasswordSalt = auth.Salt;

        var result = await _usuarioRepository.CadastrarAsync(usuario);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar um usuário");

        return Result<int>.Ok(result);
    }

    public async Task<Result<UsuarioTokenModel>> LoginAsync(Usuario usuario)
    {
        Log.Information("Cadastrando usuario");
        var usuarioResponse = await _usuarioRepository.GetUsuarioByUsernameAsync(usuario.Username);

        if (usuarioResponse is null)
            return Result<UsuarioTokenModel>.NotFoundResult();

        if (!_cryptographyProvider.VerifyPassword(usuario.Password, usuarioResponse.PasswordHash, usuarioResponse.PasswordSalt))
            return Result<UsuarioTokenModel>.Error("Senha inválida");

        var cacheKey = GetCacheKey(usuario.Username);

        if (_memoryCache.TryGetValue(cacheKey, out UsuarioTokenModel tokenCache))
            return Result<UsuarioTokenModel>.Ok(tokenCache);

        var token = _jwtProvider.Generate(usuarioResponse);

        _memoryCache.Set(cacheKey, token, TimeSpan.FromHours(4));

        return Result<UsuarioTokenModel>.Ok(token);
    }

    public Result<bool> ValidateToken(string token)
    {
        var validate = _jwtProvider.ValidateToken(token);
        
        return Result<bool>.Ok(validate);
    }

    private string GetCacheKey(string username) => $"UsuarioToken_{username}";
}
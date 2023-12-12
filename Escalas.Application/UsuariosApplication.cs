﻿using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Serilog;

namespace Escalas.Application;

public class UsuariosApplication : IUsuariosApplication
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsuariosRepository _usuarioRepository;
    private readonly ICryptographyProvider _cryptographyProvider;
    public UsuariosApplication(IJwtProvider jwtProvider, IUsuariosRepository usuarioRepository, ICryptographyProvider cryptographyProvider)
    {
        _jwtProvider = jwtProvider;
        _usuarioRepository = usuarioRepository;
        _cryptographyProvider = cryptographyProvider;
    }

    public async Task<Result<int>> CadastrarAsync(Usuario usuario)
    {
        Log.Information("Cadastrando usuario");

        var hash = _cryptographyProvider.HashPasword(usuario.Password, out var salt);
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

        if (!_cryptographyProvider.VerifyPassword(usuario.Password, result.PasswordHash, result.PasswordSalt))
            return Result<UsuarioTokenModel>.Error("Senha inválida");

        return Result<UsuarioTokenModel>.Ok(_jwtProvider.Generate(result));
    }
}
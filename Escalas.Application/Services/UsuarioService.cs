using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Serilog;

namespace Escalas.Application.Services;

public class UsuarioService : IUsuariosService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICargoRepository _cargoRepository;
    private readonly ICryptographyProvider _cryptographyProvider;
    public UsuarioService(IJwtProvider jwtProvider, IUsuarioRepository usuarioRepository, ICargoRepository cargoRepository, ICryptographyProvider cryptographyProvider)
    {
        _jwtProvider = jwtProvider;
        _usuarioRepository = usuarioRepository;
        _cargoRepository = cargoRepository;
        _cryptographyProvider = cryptographyProvider;
    }

    public async Task<Result<int>> AtribuirCargoAsync(int userId, int roleId)
    {
        if (userId == 0 || roleId == 0)
            return Result<int>.Error("É necessário especificar um id");

        var userDb = await _usuarioRepository.GetUsuarioByIdAsync(userId);

        if (userDb is null)
            return Result<int>.Error("Usuario não encontrado");

        var roleDb = await _cargoRepository.GetCargosByIdAsync(roleId);

        if (roleDb is null)
            return Result<int>.Error("Cargo não encontrado");

        Log.Information("Atribuindo um cargo");

        userDb.Cargo = roleDb;

        Serialize(userDb);

        var result = await _usuarioRepository.AtualizarCargoUsuarioAsync(userDb);

        if (result <= 0)
            return Result<int>.Error("Erro ao atribuir um cargo");

        return Result<int>.Ok(result);
    }

    public async Task<Result<int>> CadastrarAsync(Usuario usuario)
    {
        Serialize(usuario);

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
        Deserialize(usuario);

        Log.Information("Login de usuario");
        var result = await _usuarioRepository.GetUsuarioByUsernameAsync(usuario.Username);

        if (result is null)
            return Result<UsuarioTokenModel>.Error("Usuário não encontrado");

        if (!_cryptographyProvider.VerifyPassword(usuario.Password, result.PasswordHash, result.PasswordSalt))
            return Result<UsuarioTokenModel>.Error("Senha inválida");

        return Result<UsuarioTokenModel>.Ok(_jwtProvider.Generate(result));
    }

    public Result<bool> ValidateToken(string token)
    {
        var validate = _jwtProvider.ValidateToken(token);

        return Result<bool>.Ok(validate);
    }

    private void Deserialize(Usuario usuario)
    {
        usuario.Cargo = JsonConvert.DeserializeObject<Cargo>(usuario.CargoJson);
    }

    private void Serialize(Usuario usuario)
    {
        var cargo = usuario.Cargo;

        usuario.CargoJson = JsonConvert.SerializeObject(cargo);
    }
}
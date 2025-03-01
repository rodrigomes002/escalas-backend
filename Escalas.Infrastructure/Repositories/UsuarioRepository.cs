using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public UsuarioRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
    }

    public async Task<int> AtualizarCargoUsuarioAsync(Usuario usuario, Cargo cargo)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = UsuarioScripts.UpdateCargoUsuario;

        var parametros = new
        {
            id = usuario.Id,
            id_cargo = cargo.Id
        };

        return await conexao.ExecuteAsync(sql, parametros);
    }

    public async Task<int> CadastrarAsync(Usuario usuario)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = UsuarioScripts.InsertUsuario;

        var parametros = new
        {
            username = usuario.Username,
            password_hash = usuario.PasswordHash,
            password_salt = usuario.PasswordSalt,
            id_cargo = usuario.IdCargo,
            created = usuario.Created,
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parametros);
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = UsuarioScripts.SelectUsuarioById;

        return await conexao.QueryFirstOrDefaultAsync<Usuario>(sql, new { id });
    }

    public async Task<Usuario> GetUsuarioByUsernameAsync(string username)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = UsuarioScripts.SelectUsuario;

        var parametros = new
        {
            username = username
        };

        return await conexao.QueryFirstOrDefaultAsync<Usuario>(sql, parametros);
    }
}
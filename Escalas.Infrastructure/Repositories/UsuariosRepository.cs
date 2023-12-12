using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public UsuariosRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
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
            created = usuario.Created,
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parametros);
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
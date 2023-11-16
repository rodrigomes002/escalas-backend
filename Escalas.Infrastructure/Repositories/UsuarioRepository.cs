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

    public async Task<int> CadastrarUsuarioAsync(Usuario usuario)
    {
     using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

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

    public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
    {
        using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = UsuarioScripts.SelectUsuarios;

        return await conexao.QueryAsync<Usuario>(sql);
    }
}
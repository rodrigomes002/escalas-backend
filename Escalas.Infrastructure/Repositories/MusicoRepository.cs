using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories;

public class MusicoRepository : IMusicoRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public MusicoRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
    }

    public async Task AtualizarMusicoAsync(int id, Musico musico)
    {
        using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());
        
        var sql = MusicoScripts.UpdateMusico;

        using var cmd = new NpgsqlCommand(sql, conexao);
        using var reader = await cmd.ExecuteReaderAsync();
    }

    public async Task CadastrarMusicoAsync(Musico musico)
    {
       using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.SelectMusicos;

        var parametros = new
        {
            nome = musico.Nome,
            id_instrumento = musico.Instrumento
        };

        await conexao.ExecuteAsync(sql, parametros);
    }

    public Task DeletarMusicoAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Musico>> GetMusicosAsync()
    {
        using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.SelectMusicos;

        var musicos = await conexao.QueryAsync<Musico>(sql);
        return musicos;
    }
}
using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories;

public class MusicaRepository : IMusicaRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public MusicaRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
    }

    public async Task<int> CadastrarMusicaAsync(Musica musica)
    {
        var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.InsertMusica;

        var parametros = new
        {
            nome = musica.Nome,
            cantor = musica.Cantor,
            tom = musica.Tom,
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parametros);
    }

    public async Task<IEnumerable<Musica>> GetMusicasAsync()
    {
        var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.SelectMusicas;

        var musicas = await conexao.QueryAsync<Musica>(sql);
        return musicas;
    }
}
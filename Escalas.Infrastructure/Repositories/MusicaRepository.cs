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

    public async Task<int> AtualizarMusicaAsync(Musica musica)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.UpdateMusica;
        
        var parameters = new
        {
            nome = musica.Nome,
            cantor = musica.Cantor,
            tom = musica.Tom,
            id = musica.Id,
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parameters);
    }

    public async Task<int> CadastrarMusicaAsync(Musica musica)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());
        var sql = MusicaScripts.InsertMusica;

        var parameters = new
        {
            cantor = musica.Cantor,
            nome = musica.Nome,
            tom = musica.Tom
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parameters);
    }

    public async Task<int> DeletarMusicaAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.DeleteMusica;

        return await conexao.ExecuteAsync(sql, new { id });
    }

    public async Task<Musica> GetMusicaByIdAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.SelectMusicaById;

        return await conexao.QueryFirstOrDefaultAsync<Musica>(sql, new { id });
    }

    public async Task<IEnumerable<Musica>> GetMusicasAsync()
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicaScripts.SelectMusicas;

        return await conexao.QueryAsync<Musica>(sql);
    }
}
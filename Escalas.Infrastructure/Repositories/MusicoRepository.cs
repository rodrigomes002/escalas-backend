using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;
using System.Text;

namespace Escalas.Infrastructure.Repositories;

public class MusicoRepository : IMusicoRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public MusicoRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
    }

    public async Task<int> AtualizarMusicoAsync(Musico musico)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.UpdateMusico;

        var parameters = new
        {
            id = musico.Id,
            nome = musico.Nome,
            funcao = musico.Funcao,
        };

        return await conexao.QueryFirstOrDefaultAsync<int>(sql, parameters);
    }

    public async Task<int> CadastrarMusicoAsync(Musico musico)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.InsertMusico;

        var parametros = new
        {
            nome = musico.Nome,
            funcao = (int)musico.Funcao,
        };

        return await conexao.ExecuteAsync(sql, parametros);
    }

    public async Task<int> DeletarMusicoAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.DeleteMusico;

        return await conexao.ExecuteAsync(sql, new { id });
    }
    public async Task<Musico> GetMusicoByIdAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.SelectMusicoById;

        return await conexao.QueryFirstOrDefaultAsync<Musico>(sql, new { id });
    }

    public async Task<PaginatedBase<Musico>> GetMusicosAsync(int pageNumber, int pageSize, string? nome)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var countSql = MusicoScripts.CountMusicos;
        var count = await conexao.QueryFirstOrDefaultAsync<int>(countSql);

        var sql = MusicoScripts.SelectMusicos;

        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(sql);

        if (nome is not null)
            stringBuilder.AppendLine($"WHERE lower(nome) LIKE @nome");

        stringBuilder.AppendLine("LIMIT @pageSize OFFSET @pageNumber");

        var parametros = new
        {
            Nome = $"%{nome?.ToLower()}%",
            PageNumber = (pageNumber - 1) * pageSize,
            PageSize = pageSize
        };

        var musicos = await conexao.QueryAsync<Musico>(stringBuilder.ToString(), parametros);

        var paginatedBase = new PaginatedBase<Musico>
        {
            Items = musicos.ToList(),
            TotalCount = count
        };

        return paginatedBase;
    }
}
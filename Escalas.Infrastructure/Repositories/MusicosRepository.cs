﻿using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories;

public class MusicosRepository : IMusicosRepository
{
    private readonly IConnectionStringConfiguration _connectionStringConfiguration;

    public MusicosRepository(IConnectionStringConfiguration connectionStringConfiguration)
    {
        _connectionStringConfiguration = connectionStringConfiguration;
    }

    public async Task<int> AtualizarMusicoAsync(Musico musico)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.UpdateMusico;

        var parameters = new
        {
            nome = musico.Nome,
            id = musico.Id,
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
            instrumento = (int)musico.Instrumento,
        };

        return await conexao.ExecuteAsync(sql, parametros);
    }

    public async Task<int> DeletarMusicoAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.DeleteMusico;

        return await conexao.QueryFirstOrDefaultAsync(sql, new { id });
    }
    public async Task<Musico> GetMusicoByIdAsync(int id)
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.SelectMusicoById;

        return await conexao.QueryFirstOrDefaultAsync<Musico>(sql, new { id });
    }

    public async Task<IEnumerable<Musico>> GetMusicosAsync()
    {
        await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

        var sql = MusicoScripts.SelectMusicos;

        return await conexao.QueryAsync<Musico>(sql);
    }
}
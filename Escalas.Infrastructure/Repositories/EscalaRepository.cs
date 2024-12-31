using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Newtonsoft.Json;
using Npgsql;
using System.Text;

namespace Escalas.Infrastructure.Repositories
{
    public class EscalaRepository : IEscalaRepository
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        public EscalaRepository(IConnectionStringConfiguration connectionStringConfiguration)
        {
            _connectionStringConfiguration = connectionStringConfiguration;
        }

        public async Task<int> AtualizarEscalaAsync(Escala escala)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.UpdateEscala;

            var parametros = new
            {
                Id = escala.Id,
                Data = escala.Data.Date,
                MusicasManha = escala.MusicasManhaJson,
                MusicasNoite = escala.MusicasNoiteJson,
                Instrumental = escala.InstrumentalJson,
                Vocal = escala.VocalJson
            };

            return await conexao.ExecuteAsync(sql, parametros);
        }

        public async Task<int> CadastrarEscalaAsync(Escala escala)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.InsertEscala;

            var parametros = new
            {
                Data = escala.Data.Date,
                MusicasManha = escala.MusicasManhaJson,
                MusicasNoite = escala.MusicasNoiteJson,
                Instrumental = escala.InstrumentalJson,
                Vocal = escala.VocalJson
            };

            return await conexao.ExecuteAsync(sql, parametros);
        }

        public async Task<int> DeletarEscalaAsync(int id)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.DeleteEscala;

            return await conexao.ExecuteAsync(sql, new { id });
        }

        public async Task<Escala> GetEscalaByIdAsync(int id)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.SelectEscalaById;

            return await conexao.QueryFirstOrDefaultAsync<Escala>(sql, new { id });
        }

        public async Task<PaginatedBase<Escala>> GetEscalasAsync(int pageNumber, int pageSize, string? data)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var countSql = EscalaScripts.CountEscalas;
            var count = await conexao.QueryFirstOrDefaultAsync<int>(countSql);

            var sql = EscalaScripts.SelectEscala;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(sql);

            if (data is not null)
                stringBuilder.AppendLine($"WHERE data::date = @data");

            stringBuilder.AppendLine("LIMIT @pageSize OFFSET @pageNumber");

            var parametros = new
            {
                Data = Convert.ToDateTime(data),
                PageNumber = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            };

            var escalas = await conexao.QueryAsync<Escala>(stringBuilder.ToString(), parametros);

            var paginatedBase = new PaginatedBase<Escala>
            {
                Items = escalas.ToList(),
                TotalCount = count
            };

            return paginatedBase;
        }
    }
}

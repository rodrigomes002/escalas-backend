using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Newtonsoft.Json;
using Npgsql;

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
                Data = escala.Data,
                Turno = escala.Turno,
                Repertorio = escala.RepertorioJson,
                Participantes = escala.ParticipantesJson
            };

            return await conexao.ExecuteAsync(sql, parametros);
        }

        public async Task<int> CadastrarEscalaAsync(Escala escala)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.InsertEscala;

            var parametros = new
            {
                Data = escala.Data,
                Turno = escala.Turno,
                Repertorio = escala.RepertorioJson,
                Participantes = escala.ParticipantesJson
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

        public async Task<IEnumerable<Escala>> GetEscalasAsync()
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.SelectEscala;

            return await conexao.QueryAsync<Escala>(sql);
        }
    }
}

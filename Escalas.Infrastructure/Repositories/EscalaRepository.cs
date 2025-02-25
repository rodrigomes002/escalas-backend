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

        public async Task<IEnumerable<Escala>> GetEscalasAsync()
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = EscalaScripts.SelectEscala;

            var now = DateTime.Now;    
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);
            var lastDayOfMonth = firstDayOfMonth.AddDays(-1);
            
            var daysUntilSunday = (int)lastDayOfMonth.DayOfWeek;
            var daysToSubtract = (daysUntilSunday == 0) ? 0 : daysUntilSunday;
        
            var lastSunday = lastDayOfMonth.AddDays(-daysToSubtract);  

            var parametros = new
            {
                month = now.Date > lastSunday.Date ? DateTime.Now.AddMonths(1).Date.Month : DateTime.Now.Date.Month,
                year = now.Date > lastSunday.Date ? DateTime.Now.AddMonths(1).Date.Year : DateTime.Now.Date.Year,
            };

            var escalas = await conexao.QueryAsync<Escala>(sql, parametros);

            return escalas;
        }
    }
}

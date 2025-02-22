using Dapper;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Scripts;
using Npgsql;

namespace Escalas.Infrastructure.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        public CargoRepository(IConnectionStringConfiguration connectionStringConfiguration)
        {
            _connectionStringConfiguration = connectionStringConfiguration;
        }

        public async Task<int> AtualizarCargoAsync(Cargo cargo)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = CargoScripts.UpdateCargo;

            var parametros = new
            {
                id = cargo.Id,
                nome = cargo.Nome,
                nivel_autorizacao = cargo.NivelAutorizacao
            };

            return await conexao.QueryFirstOrDefaultAsync<int>(sql, parametros);
        }

        public async Task<int> CadastrarCargoAsync(Cargo cargo)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = CargoScripts.InsertCargo;

            var parametros = new
            {
                nome = cargo.Nome,
                nivel_autorizacao = (int)cargo.NivelAutorizacao
            };

            return await conexao.ExecuteAsync(sql, parametros);
        }

        public async Task<int> DeletarCargoAsync(int id)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = CargoScripts.DeleteCargo;

            return await conexao.ExecuteAsync(sql, new { id });
        }

        public async Task<PaginatedBase<Cargo>> GetCargosAsync(int pageNumber, int pageSize, string? nome)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = CargoScripts.SelectCargos;

            var parametros = new
            {
                Nome = $"%{nome?.ToLower()}%",
                PageNumber = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            };

            var multiple = await conexao.QueryMultipleAsync(sql, parametros);
            var totalCount = await multiple.ReadFirstAsync<int>();
            var cargos = (await multiple.ReadAsync<Cargo>()).AsList();

            var paginatedBase = new PaginatedBase<Cargo>
            {
                Items = cargos.ToList(),
                TotalCount = totalCount
            };

            return paginatedBase;
        }

        public async Task<Cargo> GetCargosByIdAsync(int id)
        {
            await using var conexao = new NpgsqlConnection(_connectionStringConfiguration.GetPostgresqlConnectionString());

            var sql = CargoScripts.SelectCargoById;

            return await conexao.QueryFirstOrDefaultAsync<Cargo>(sql, new { id });
        }
    }
}

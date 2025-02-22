using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Application.Interfaces
{
    public interface ICargoService
    {
        Task<Result<PaginatedBase<Cargo>>> GetCargosAsync(int pageNumber, int pageSize, string? nome);
        Task<Result<Cargo>> GetCargosByIdAsync(int id);
        Task<Result<int>> CadastrarCargoAsync(Cargo cargo);
        Task<Result<int>> AtualizarCargoAsync(int id, Cargo cargo);
        Task<Result<int>> DeletarCargoAsync(int id);
    }
}

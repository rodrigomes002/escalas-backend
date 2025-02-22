using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Interfaces
{
    public interface ICargoRepository
    {
        Task<PaginatedBase<Cargo>> GetCargosAsync(int pageNumber, int pageSize, string? nome);
        Task<Cargo> GetCargosByIdAsync(int id);
        Task<int> CadastrarCargoAsync(Cargo cargo);
        Task<int> AtualizarCargoAsync(Cargo cargo);
        Task<int> DeletarCargoAsync(int id);
    }
}

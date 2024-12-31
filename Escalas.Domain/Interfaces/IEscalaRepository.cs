using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Interfaces;

public interface IEscalaRepository
{
    Task<PaginatedBase<Escala>> GetEscalasAsync(int pageNumber, int pageSize, string? data);
    Task<Escala> GetEscalaByIdAsync(int id);
    Task<int> CadastrarEscalaAsync(Escala escala);
    Task<int> AtualizarEscalaAsync(Escala escala);
    Task<int> DeletarEscalaAsync(int id);
}


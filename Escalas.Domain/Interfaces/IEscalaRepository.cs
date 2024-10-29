using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces;

public interface IEscalaRepository
{
    Task<IEnumerable<Escala>> GetEscalasAsync();
    Task<Escala> GetEscalaByIdAsync(int id);
    Task<int> CadastrarEscalaAsync(Escala escala);
    Task<int> AtualizarEscalaAsync(Escala escala);
    Task<int> DeletarEscalaAsync(int id);
}


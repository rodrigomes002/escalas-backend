using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Interfaces;

public interface IMusicoRepository
{
    Task<PaginatedBase<Musico>> GetMusicosAsync(int pageNumber, int pageSize, string? nome);
    Task<Musico> GetMusicoByIdAsync(int id);
    Task<int> CadastrarMusicoAsync(Musico musico);
    Task<int> AtualizarMusicoAsync(Musico musico);
    Task<int> DeletarMusicoAsync(int id);
}
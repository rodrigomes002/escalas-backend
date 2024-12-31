using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Interfaces;

public interface IMusicaRepository
{
    Task<PaginatedBase<Musica>> GetMusicasAsync(int pageNumber, int pageSize, string? nome);
    Task<Musica> GetMusicaByIdAsync(int id);
    Task<int> CadastrarMusicaAsync(Musica musica);
    Task<int> AtualizarMusicaAsync(Musica musica);
    Task<int> DeletarMusicaAsync(int id);
}

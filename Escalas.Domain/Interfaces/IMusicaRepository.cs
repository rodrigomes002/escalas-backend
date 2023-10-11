using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces;

public interface IMusicaRepository
{
    Task<IEnumerable<Musica>> GetMusicasAsync();
    Task<Musica> GetMusicaByIdAsync(int id);
    Task<int> CadastrarMusicaAsync(Musica musica);
    Task<int> AtualizarMusicaAsync(Musica musica);
}

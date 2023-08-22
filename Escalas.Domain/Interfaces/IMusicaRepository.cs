using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces;

public interface IMusicaRepository
{
    Task<IEnumerable<Musica>> GetMusicasAsync();
    Task<int> CadastrarMusicaAsync(Musica musica);
}

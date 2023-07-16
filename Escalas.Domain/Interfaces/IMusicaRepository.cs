using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces
{
    public interface IMusicaRepository
    {
        Task<IEnumerable<Musica>> GetMusicasAsync();
        Task CadastrarMusicaAsync(Musica musica);
    }
}

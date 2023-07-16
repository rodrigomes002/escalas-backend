using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces
{
    public interface IMusicaService
    {
        Task<IEnumerable<Musica>> GetMusicasAsync();
        Task CadastrarMusicaAsync(MusicaRequest request);
    }
}

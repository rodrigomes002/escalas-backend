using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces;

public interface IMusicoRepository
{
    Task<IEnumerable<Musico>> GetMusicosAsync();
    Task CadastrarMusicoAsync(Musico musico);
}

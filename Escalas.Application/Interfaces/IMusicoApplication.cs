using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;

public interface IMusicoApplication
{
    Task<IEnumerable<Musico>> GetMusicosAsync();
    Task CadastrarMusicoAsync(MusicoModel model);
}

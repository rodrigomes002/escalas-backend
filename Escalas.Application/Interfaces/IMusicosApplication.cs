using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;

public interface IMusicosApplication
{
    Task<IEnumerable<Musico>> GetMusicosAsync();
    Task CadastrarMusicoAsync(MusicoModel model);
}

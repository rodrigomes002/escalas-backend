using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces;

public interface IMusicosRepository
{
    Task<IEnumerable<Musico>> GetMusicosAsync();
    Task<Musico> GetMusicoByIdAsync(int id);
    Task<int> CadastrarMusicoAsync(Musico musico);
    Task<int> AtualizarMusicoAsync(Musico musico);
    Task<int> DeletarMusicoAsync(int id);
}
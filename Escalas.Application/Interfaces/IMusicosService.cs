using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;

public interface IMusicosService
{
    Task<Result<IEnumerable<Musico>>> GetMusicosAsync();
    Task<Result<Musico>> GetMusicoByIdAsync(int id);
    Task<Result<int>> CadastrarMusicoAsync(Musico musico);
    Task<Result<int>> AtualizarMusicoAsync(int id, Musico musico);
    Task<Result<int>> DeletarMusicoAsync(int id);
}

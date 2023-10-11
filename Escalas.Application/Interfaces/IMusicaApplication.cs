using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;

public interface IMusicaApplication
{
    Task<Result<IEnumerable<Musica>>> GetMusicasAsync();
    Task<Result<Musica>> GetMusicaByIdAsync(int id);
    Task<Result<int>> CadastrarMusicaAsync(Musica musica);
    Task<Result<int>> AtualizarMusicaAsync(int id, Musica musica);
}

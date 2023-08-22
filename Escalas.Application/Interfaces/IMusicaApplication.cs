using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;

public interface IMusicaApplication
{
    Task<Result<IEnumerable<Musica>>> GetMusicasAsync();
    Task<Result<int>> CadastrarMusicaAsync(Musica musica);
}

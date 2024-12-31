using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Application.Interfaces;

public interface IMusicasService
{
    Task<Result<PaginatedBase<Musica>>> GetMusicasAsync(int pageNumber, int pageSize, string? nome);
    Task<Result<Musica>> GetMusicaByIdAsync(int id);
    Task<Result<int>> CadastrarMusicaAsync(Musica musica);
    Task<Result<int>> AtualizarMusicaAsync(int id, Musica musica);
    Task<Result<int>> DeletarMusicaAsync(int id);
}

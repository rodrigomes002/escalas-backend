using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Application.Interfaces;

public interface IMusicosService
{
    Task<Result<PaginatedBase<Musico>>> GetMusicosAsync(int pageNumber, int pageSize, string? nome);
    Task<Result<Musico>> GetMusicoByIdAsync(int id);
    Task<Result<int>> CadastrarMusicoAsync(Musico musico);
    Task<Result<int>> AtualizarMusicoAsync(int id, Musico musico);
    Task<Result<int>> DeletarMusicoAsync(int id);
}

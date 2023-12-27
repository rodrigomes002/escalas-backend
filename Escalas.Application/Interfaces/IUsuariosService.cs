using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces
{
    public interface IUsuariosService
    {
        Task<Result<int>> CadastrarAsync(Usuario usuario);
        Task<Result<UsuarioTokenModel>> LoginAsync(Usuario usuario);
    }
}
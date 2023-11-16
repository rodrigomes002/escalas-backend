using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<Result<int>> CadastrarUsuarioAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
    }
}
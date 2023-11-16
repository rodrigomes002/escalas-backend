using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<int> CadastrarUsuarioAsync(Usuario usuario);
    }
}
using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByUsernameAsync(string username);
        Task<int> CadastrarAsync(Usuario usuario);
    }
}
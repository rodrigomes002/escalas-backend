using Escalas.Domain.Entities;

namespace Escalas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByUsernameAsync(string username);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<int> CadastrarAsync(Usuario usuario);
        Task<int> AtualizarCargoUsuarioAsync(Usuario usuario);
    }
}
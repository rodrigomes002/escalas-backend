using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces
{
    public interface IUsuariosService
    {
        Task<Result<int>> CadastrarAsync(Usuario usuario);
        Task<Result<int>> AtribuirCargoAsync(int userId, int roleId);
        Task<Result<UsuarioTokenModel>> LoginAsync(Usuario usuario);
        Result<bool> ValidateToken(string token);
    }
}
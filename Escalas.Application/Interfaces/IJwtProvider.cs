using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Interfaces;
public interface IJwtProvider
{
    UsuarioTokenModel Generate(Usuario user);
    bool ValidateToken(string token);
}
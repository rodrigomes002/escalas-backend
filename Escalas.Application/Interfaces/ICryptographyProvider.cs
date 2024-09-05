using Escalas.Application.Models;

namespace Escalas.Application.Interfaces;
public interface ICryptographyProvider
{
    AuthModel HashPasword(string password);
    bool VerifyPassword(string password, string hash, byte[] salt);
}

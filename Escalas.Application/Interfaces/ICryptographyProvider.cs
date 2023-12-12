namespace Escalas.Application.Interfaces;
public interface ICryptographyProvider
{
    string HashPasword(string password, out byte[] salt);
    bool VerifyPassword(string password, string hash, byte[] salt);
}

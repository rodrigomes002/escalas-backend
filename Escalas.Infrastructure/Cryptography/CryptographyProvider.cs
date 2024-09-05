using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using System.Security.Cryptography;
using System.Text;

namespace Escalas.Infrastructure.Cryptography;
public class CryptographyProvider : ICryptographyProvider
{
    const int keySize = 64;
    const int iterations = 350000;
    HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public AuthModel HashPasword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
        salt,
            iterations,
            hashAlgorithm,
            keySize);

        var auth = new AuthModel()
        {
            Hash = Convert.ToHexString(hash),
            Salt = salt
        };

        return auth;
    }

    public bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}

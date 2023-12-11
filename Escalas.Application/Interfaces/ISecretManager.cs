namespace Escalas.Application.Interfaces;
public interface ISecretManager
{
    public Task<TResult?> GetSecretValuesAsync<TResult>(string secretName);
}
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Escalas.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Escalas.Infrastructure.SecretManager;
public sealed class SecretManager : ISecretManager
{
    private readonly IConfiguration _configuration;
    public SecretManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TResult?> GetSecretValuesAsync<TResult>(string secretName)
    {
        var region = _configuration["AWS:Region"];

        var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        var request = new GetSecretValueRequest { SecretId = secretName, VersionStage = "AWSCURRENT", };

        var response = await client.GetSecretValueAsync(request);

        string secret = response.SecretString;

        return JsonSerializer.Deserialize<TResult>(secret, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}

using Escalas.Application.Interfaces;
using Escalas.Domain.Entities.Secret;
using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Escalas.Infrastructure.DbConfiguration;

public class ConnectionStringConfiguration : IConnectionStringConfiguration
{
    private readonly IConfiguration _configuration;
    private readonly ISecretManager _secretManager;

    private string _connectionString = null!;
    private string _readOnlyConnectionString = null!;

    public ConnectionStringConfiguration(IConfiguration configuration, ISecretManager secretManager)
    {
        _configuration = configuration;
        _secretManager = secretManager;
    }

    public async Task CreateConnectionString()
    {
        var secretValue = await _secretManager.GetSecretValuesAsync<DbSecret>(_configuration["Database:SecretName"] ?? string.Empty);

        _connectionString = _configuration["Database:ConnectionString"] ?? string.Empty;

        if (!string.IsNullOrEmpty(_connectionString))
            _connectionString = _connectionString.Replace("Secret", secretValue?.Password);
    }

    public async Task CreateReadOnlyConnectionString()
    {
        var secretValue = await _secretManager.GetSecretValuesAsync<DbSecret>(_configuration["Database:SecretName"] ?? string.Empty);

        _readOnlyConnectionString = _configuration["Database:ConnectionString"] ?? string.Empty;

        if (!string.IsNullOrEmpty(_readOnlyConnectionString))
            _readOnlyConnectionString = _readOnlyConnectionString.Replace("Secret", secretValue?.Password);
    }

    public async Task<string> GetConnectionString(bool readOnly)
    {
        if (readOnly)
        {
            await CreateReadOnlyConnectionString();
            return _readOnlyConnectionString;
        }

        await CreateConnectionString();
        return _connectionString;
    }
}
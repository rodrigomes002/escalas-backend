using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Escalas.Infrastructure.DbConfiguration;

public class ConnectionStringConfiguration : IConnectionStringConfiguration
{
    private readonly IConfiguration _configuration;

    public ConnectionStringConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetPostgresqlConnectionString()
    {
        return _configuration["Database:Postgresql"];
    }
}

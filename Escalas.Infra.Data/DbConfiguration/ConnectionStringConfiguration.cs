using Escalas.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Escalas.Infra.Data.DbConfiguration
{
    public class ConnectionStringConfiguration : IConnectionStringConfiguration
    {
        private readonly IConfiguration _configuration;

        private string _postgresqlConnectionString;
        public ConnectionStringConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetPostgresqlConnectionString()
        {
            _postgresqlConnectionString = _configuration["Database:Postgresql"];

            return _postgresqlConnectionString;
        }
    }
}

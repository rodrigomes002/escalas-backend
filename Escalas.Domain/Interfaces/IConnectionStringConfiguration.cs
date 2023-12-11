namespace Escalas.Domain.Interfaces;

public interface IConnectionStringConfiguration
{
    Task<string> GetConnectionString(bool readOnly);
    Task CreateConnectionString();
    Task CreateReadOnlyConnectionString();
}

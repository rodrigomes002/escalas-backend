namespace Escalas.Domain.Interfaces
{
    public interface IConnectionStringConfiguration
    {
        string GetPostgresqlConnectionString();
    }
}

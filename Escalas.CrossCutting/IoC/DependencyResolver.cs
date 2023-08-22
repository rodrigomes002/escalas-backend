using Escalas.Application;
using Escalas.Application.Interfaces;
using Escalas.CrossCutting.MapperAssembly;
using Escalas.Domain.Interfaces;
using Escalas.Infra.Data.DbConfiguration;
using Escalas.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Escalas.Infra.CrossCutting.IoC;

public static class DependencyResolver
{
    public static void AddDependencyResolver(this IServiceCollection services)
    {
        RegisterApplication(services);
        RegisterInfrastructure(services);
        services.AddAutoMapper(MapperAssembly.GetMapperAssemblies());
    }

    private static void RegisterApplication(IServiceCollection services)
    {
        services.AddScoped<IMusicaApplication, MusicaApplication>();
        services.AddScoped<IMusicoApplication, MusicoApplication>();
    }

    private static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>();
        services.AddScoped<IMusicaRepository, MusicaRepository>();
        services.AddScoped<IMusicoRepository, MusicoRepository>();
    }
}

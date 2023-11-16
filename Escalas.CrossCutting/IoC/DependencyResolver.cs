using Escalas.Application;
using Escalas.Application.Interfaces;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.DbConfiguration;
using Escalas.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Escalas.CrossCutting.IoC;

public static class DependencyResolver
{
    public static void AddDependencyResolver(this IServiceCollection services)
    {
        RegisterApplication(services);
        RegisterInfrastructure(services);
        //alterar para tiny mapper
        services.AddAutoMapper(MapperAssembly.MapperAssembly.GetMapperAssemblies());
    }

    private static void RegisterApplication(IServiceCollection services)
    {
        services.AddScoped<IMusicaApplication, MusicaApplication>();
        services.AddScoped<IMusicoApplication, MusicoApplication>();
        services.AddScoped<IUsuarioApplication, UsuarioApplication>();
    }

    private static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>();
        services.AddScoped<IMusicaRepository, MusicaRepository>();
        services.AddScoped<IMusicoRepository, MusicoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}

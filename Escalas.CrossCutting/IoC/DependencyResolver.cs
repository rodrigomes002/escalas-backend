using Escalas.Application;
using Escalas.Application.Interfaces;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Authentication;
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
        services.AddScoped<IMusicasApplication, MusicasApplication>();
        services.AddScoped<IMusicosApplication, MusicosApplication>();
        services.AddScoped<IUsuariosApplication, UsuariosApplication>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }

    private static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>();
        services.AddScoped<IMusicaRepository, MusicaRepository>();
        services.AddScoped<IMusicoRepository, MusicoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}

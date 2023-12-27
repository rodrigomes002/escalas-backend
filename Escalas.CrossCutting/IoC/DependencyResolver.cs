using Escalas.Application.Interfaces;
using Escalas.Application.Services;
using Escalas.Domain.Interfaces;
using Escalas.Infrastructure.Authentication;
using Escalas.Infrastructure.Cryptography;
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
        services.AddScoped<IMusicasService, MusicaService>();
        services.AddScoped<IMusicosService, MusicoService>();
        services.AddScoped<IUsuariosService, UsuarioService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddSingleton<ICryptographyProvider, CryptographyProvider>();
    }

    private static void RegisterInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>();
        services.AddScoped<IMusicaRepository, MusicaRepository>();
        services.AddScoped<IMusicoRepository, MusicoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}

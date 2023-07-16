using Escalas.Application;
using Escalas.Application.Interfaces;
using Escalas.Domain.Interfaces;
using Escalas.Infra.Data.DbConfiguration;
using Escalas.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Escalas.Infra.CrossCutting.IoC
{
    public static class DependencyResolver
    {
        public static void Resolver(IServiceCollection services)
        {
            RegisterApplication(services);
            RegisterInfrastructure(services);
        }

        private static void RegisterApplication(IServiceCollection services)
        {
            services.AddScoped<IMusicaService, MusicaService>();
        }

        private static void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddScoped<IMusicaRepository, MusicaRepository>();
            services.AddScoped<IConnectionStringConfiguration, ConnectionStringConfiguration>();
        }
    }
}

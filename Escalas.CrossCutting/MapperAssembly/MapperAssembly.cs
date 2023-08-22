using System.Reflection;

namespace Escalas.CrossCutting.MapperAssembly;
public class MapperAssembly
{
    public static IEnumerable<Assembly> GetMapperAssemblies()
    {
        return new[]
        {
            Assembly.Load("Escalas.API"),
            Assembly.Load("Escalas.Application"),
            Assembly.Load("Escalas.CrossCutting"),
            Assembly.Load("Escalas.Domain"),
            Assembly.Load("Escalas.Infrastructure")
        };
    }
}

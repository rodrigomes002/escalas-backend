using Escalas.Domain.Entities;

namespace Escalas.Tests.Mocks.Entities;

public class MusicaMock
{
    public static IEnumerable<Musica> Musicas()
    {
        return new List<Musica>()
        {
            new("Espelhos mágico", "Oficina G3", "B"),
            new("Todo som", "Resgate", "G")
        };
    }

    public static Musica Musica()
    {
        return new("Espelhos mágico", "Oficina G3", "B");
    }

    public static Musica Musica_Null()
    {
        return default!;
    }
}
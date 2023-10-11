using Escalas.Application.Models;

namespace Escalas.Tests.Mocks.Models;

public class MusicaModelMock
{
    public static MusicaModel FullObject()
    {
        return new MusicaModel
        {
            Id = 1,
            Cantor = "Oficina G3",
            Nome = "Espelhos mágico",
            Tom = "B"
        };
    }

    public static MusicaModel EmptyName()
    {
        return new MusicaModel
        {
            Id = 1,
            Cantor = "Oficina G3",
            Nome = "",
            Tom = "B"
        };
    }
}
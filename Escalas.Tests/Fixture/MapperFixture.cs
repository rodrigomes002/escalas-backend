using AutoMapper;
using Escalas.Application.Mappings;

namespace Escalas.Tests.Fixture;

public class MapperFixture
{
    public MapperFixture()
    {
        var config = new MapperConfiguration(opts =>
        {
            opts.AddProfile(new MusicaMap());
        });

        Mapper = config.CreateMapper();
    }

    public IMapper Mapper { get; }
}
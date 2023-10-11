using AutoMapper;
using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Mappings;
public class MusicaMap : Profile
{
    public MusicaMap()
    {
        CreateMap<MusicaModel, Musica>()
            .ConstructUsing(x => new Musica(x.Nome, x.Cantor, x.Tom));

        CreateMap<Musica, MusicaModel>();
    }
}

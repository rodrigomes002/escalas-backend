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

        CreateMap<Musica, MusicaModel>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Cantor, m => m.MapFrom(src => src.Cantor))
            .ForMember(dest => dest.Tom, m => m.MapFrom(src => src.Tom));
    }
}

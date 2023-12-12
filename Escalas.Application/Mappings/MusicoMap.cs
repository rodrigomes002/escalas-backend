using AutoMapper;
using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Mappings;
public class MusicoMap : Profile
{
    public MusicoMap()
    {
        CreateMap<MusicoModel, Musico>()
            .ConstructUsing(x => new Musico(x.Nome, x.Instrumento));

        CreateMap<Musico, MusicoModel>();
    }
}

using AutoMapper;
using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Mappings
{
    public class EscalaMap : Profile
    {
        public EscalaMap() 
        {
            CreateMap<EscalaModel, Escala>()
                .ConstructUsing(x => new Escala(x.Data, x.Turno));

            CreateMap<Escala, EscalaModel>();
        }
    }
}

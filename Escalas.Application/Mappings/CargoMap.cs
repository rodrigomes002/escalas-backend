using AutoMapper;
using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Mappings
{
    public class CargoMap : Profile
    {
        public CargoMap()
        {
            CreateMap<CargoModel, Cargo>()
                .ConstructUsing(x => new Cargo(x.Nome, x.nivelAutorizacao));

            CreateMap<Cargo, CargoModel>();
        }
    }
}

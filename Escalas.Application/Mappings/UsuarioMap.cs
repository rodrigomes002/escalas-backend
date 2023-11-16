using AutoMapper;
using Escalas.Application.Models;
using Escalas.Domain.Entities;

namespace Escalas.Application.Mappings
{
    public class UsuarioMap : Profile
    {
        public UsuarioMap()
        {
            CreateMap<UsuarioModel, Usuario>();
        }
    }
}
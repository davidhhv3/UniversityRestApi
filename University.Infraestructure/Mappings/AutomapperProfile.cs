using AutoMapper;
using University.Core.DTOs;
using University.Core.Entities;

namespace University.Infraestructure.Mappings
{
    internal class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
        }
    }
}

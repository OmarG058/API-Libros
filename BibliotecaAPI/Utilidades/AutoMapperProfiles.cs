using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()//mapeo de datos para mostrar
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(autor => $"{autor.Nombres} {autor.Apellidos}"));

            CreateMap<AutorCreacionDTO, Autor>(); //mapeo de datos para recibir

            CreateMap<Libro, LibroDTO>(); //mapeo de datos para mostrar

            CreateMap<LibroCreacionDTO, Libro>();//mapeo de datos para recibir
        }
    }
}

using AutoMapper;
using SmartSchool.API.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            //Aluno
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
                );

            CreateMap<AlunoDTO, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDTO>().ReverseMap();

            //Professor
            CreateMap<Professor, ProfessorDTO>()
            .ForMember(
                dest => dest.Nome,
                opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
            )
            .ForMember(
                    dest => dest.TempoDeEmpresa,
                    opt => opt.MapFrom(src => src.DataIni.GetCurrentAge())
                );

            CreateMap<ProfessorDTO, Professor>();
            CreateMap<Professor, ProfessorRegistrarDTO>().ReverseMap();
        }
    }
}
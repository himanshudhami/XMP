using AutoMapper;
using XMP.Application.DTOs;
using XMP.Domain.Entities;

namespace XMP.Application.Mappers
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.KeyName, opt => opt.MapFrom(src => src.KeyName))
                .ReverseMap(); // For reverse mapping if needed
        }
    }
}

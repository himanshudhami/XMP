using AutoMapper;
using XMP.Application.DTOs;
using XMP.Domain.Entities;

namespace XMP.Application.Mappers
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StatusValue, opt => opt.MapFrom(src => src.StatusValue))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ReverseMap(); // For reverse mapping if needed
        }
    }
}

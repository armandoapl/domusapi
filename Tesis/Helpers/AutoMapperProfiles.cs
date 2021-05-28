using AutoMapper;
using Tesis.DTOs;
using Tesis.Entities;

namespace Tesis.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, AgentDto>();
            CreateMap<REProperty, PropertyDto>();
            CreateMap<Photo, PhotoDto>();
        }
    }
}

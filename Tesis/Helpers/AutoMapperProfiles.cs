using AutoMapper;
using System.Linq;
using Tesis.DTOs;
using Tesis.Entities;
using Tesis.Extensions;

namespace Tesis.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            /**AppUser (src) to AgentDto (dest)*/
            CreateMap<AppUser, AgentDto>()
                .ForMember(
                    dest => dest.PhotoUrl, opt =>
                        opt.MapFrom(src =>
                            src.Photos.FirstOrDefault(x => x.IsMain).Url)
                            )
                .ForMember(
                    dest => dest.age,
                        opt => opt.MapFrom(src =>
                            src.DateOfBirth.CalculateAge())
                            );

            CreateMap<AgentUpdateDto, AppUser>();


            /*REProperty (src) to PropertyDto (dest)*/
            CreateMap<REProperty, PropertyDto>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opt =>
                        opt.MapFrom(
                            src => src.Photos.FirstOrDefault(x => x.IsMain).Url
                            )
                );


            CreateMap<Photo, PhotoDto>();
        }
    }
}

using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Crosscutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<UserModel, UserDto>()
                .ReverseMap();

            CreateMap<UserModel, UserDtoCreate>()
               .ReverseMap();

            CreateMap<UserModel, UserDtoUpdate>()
               .ReverseMap();
        }
    }
}

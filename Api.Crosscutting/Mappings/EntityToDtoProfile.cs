using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Crosscutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDtoCreate, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();

            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();
            
            CreateMap<UserEntity, UserDto>()
                .ReverseMap();
        }
    }
}

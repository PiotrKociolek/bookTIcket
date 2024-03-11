using AutoMapper;
using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Core.Domain;

namespace Template.Modules.Core.Application.Mappers
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
using AutoMapper;
using Template.Modules.Core.Application.Mappers;

namespace Template.Api.Mappers
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreProfile>();
            });

            return config.CreateMapper();
        }
    }
}
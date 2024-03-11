using AutoMapper;
using Template.Modules.Integrations.Tauron.Application.Dto;
using Template.Modules.Integrations.Tauron.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Modules.Integrations.Tauron.Application.Mapper
{
    public class TauronProfile : Profile
    {
        public TauronProfile()
        {
            CreateMap<OutageItem, OutageDto>();
        }
    }
}

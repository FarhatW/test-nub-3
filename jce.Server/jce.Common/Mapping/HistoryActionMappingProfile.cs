using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Resources;

namespace jce.Common.Mapping
{
    public class HistoryActionMappingProfile : Profile
    {
        public HistoryActionMappingProfile()
        {
            CreateMap<HistoryAction, HistoryActionResource>();
               

            CreateMap<HistoryActionResource, HistoryAction>();
        }
    }
}

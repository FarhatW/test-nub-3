using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Resources;
using jce.Common.Resources.PintelSheet;

namespace jce.Common.Mapping
{
    class PintelSheetMappingProfile : Profile
    {
        public PintelSheetMappingProfile()
        {
            //Domaine to API Resource

            CreateMap<PintelSheet, PintelSheetResource>()
                .ForMember(psr => psr.AgeGroup, opt => opt.MapFrom(ps => AgeGroup.From(ps.AgeGroup.Id))).
                AfterMap((ps, psr) =>
                {
                    psr.ProductCount = ps.Products.Count();
                });


            CreateMap<PintelSheet, PintelSheetSaveResource>();

            //API Resource to Domaine

            CreateMap<PintelSheetResource, PintelSheet>();

            CreateMap<PintelSheetSaveResource, PintelSheet>()
            .ForMember(ps => ps.AgeGroup, opt => opt.MapFrom(pr => AgeGroup.From(pr.AgeGroupId)));
        }
    }
}

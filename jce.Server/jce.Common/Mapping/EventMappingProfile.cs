using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Mapping
{
    class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Event, EventResource>();

            //API Resource to Domaine
            //ignore : ne pas tenir compte de l'id lors d'un update et d'un enregistrement en base
            CreateMap<EventResource, Event>()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Schedules, opt => opt.Ignore())
                    .AfterMap((er, e) =>
                    {
                        var updatenew = e.Schedules;
                        foreach (var item in updatenew)
                        {
                            item.IsDelete = er.IsDelete;
                            item.IsActif = er.IsActif;
                        }
                    });
        }
    }
}

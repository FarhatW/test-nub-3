using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Mapping
{
    class MailMappingProfile : Profile
    {
        public MailMappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Mail, MailResource>();

            //API Resource to Domaine
            CreateMap<MailResource, Mail>();
        }
    }
}

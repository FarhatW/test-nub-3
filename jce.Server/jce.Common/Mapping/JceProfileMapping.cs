using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.adminProfile;
using jce.Common.Resources.CE;
using jce.Common.Resources.Child;
using jce.Common.Resources.personProfile;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Mapping
{
    class JceProfileMapping : Profile
    {
        public JceProfileMapping()
        {

            CreateMap<JceProfile, JceProfileResource>()
                .Include<AdminJceProfile, AdminProfileResource>()

                .Include<PersonJceProfile, PersonProfileResource>()
              
                .AfterMap((a, b) => b.Address = new AddressResource
                {
                    Company = a.Company,
                    City = a.City,
                    Agency = a.Agency,
                    AddressExtra = a.AddressExtra,
                    Service = a.Service,
                    Address1 = a.Address1,
                    StreetNumber = a.StreetNumber,
                    Address2 = a.Address2,
                    PostalCode = a.PostalCode

                });

           CreateMap<AdminJceProfile, AdminProfileResource>();
           CreateMap<PersonJceProfile, PersonProfileResource>();

            CreateMap<JceProfileResource, JceProfile>()
                .Include<AdminProfileResource, AdminJceProfile>()
                .Include<PersonProfileResource, PersonJceProfile>();

            CreateMap<AdminProfileResource, AdminJceProfile>();
            CreateMap<PersonProfileResource, PersonJceProfile>();

            CreateMap<Child, ChildResource>();

            CreateMap<AdminProfileSaveResource, AdminJceProfile>()
                .ForMember(ad => ad.Address1, opt => opt.MapFrom(a => a.Address.Address1))
                .ForMember(ad => ad.Address2, opt => opt.MapFrom(a => a.Address.Address2))
                .ForMember(ad => ad.StreetNumber, opt => opt.MapFrom(a => a.Address.StreetNumber))
                .ForMember(ad => ad.PostalCode, opt => opt.MapFrom(a => a.Address.PostalCode))
                .ForMember(ad => ad.City, opt => opt.MapFrom(a => a.Address.City))
                .ForMember(ad => ad.AddressExtra, opt => opt.MapFrom(a => a.Address.AddressExtra))
                .ForMember(ad => ad.Company, opt => opt.MapFrom(a => a.Address.Company))
                .ForMember(ad => ad.Service, opt => opt.MapFrom(a => a.Address.Service))
                .ForMember(ad => ad.Agency, opt => opt.MapFrom(a => a.Address.Agency));


            CreateMap<PersonProfileSaveResource, PersonJceProfile>()
                .ForMember(ad => ad.Address1, opt => opt.MapFrom(a => a.Address.Address1))
                .ForMember(ad => ad.Address2, opt => opt.MapFrom(a => a.Address.Address2))
                .ForMember(ad => ad.StreetNumber, opt => opt.MapFrom(a => a.Address.StreetNumber))
                .ForMember(ad => ad.PostalCode, opt => opt.MapFrom(a => a.Address.PostalCode))
                .ForMember(ad => ad.City, opt => opt.MapFrom(a => a.Address.City))
                .ForMember(ad => ad.AddressExtra, opt => opt.MapFrom(a => a.Address.AddressExtra))
                .ForMember(ad => ad.Company, opt => opt.MapFrom(a => a.Address.Company))
                .ForMember(ad => ad.Service, opt => opt.MapFrom(a => a.Address.Service))
                .ForMember(ad => ad.CreatedBy, opt => opt.MapFrom(a => a.CreatedBy))
                .ForMember(ad => ad.Agency, opt => opt.MapFrom(a => a.Address.Agency))
                .ForMember(ad => ad.CeId, opt => opt.MapFrom(a => a.CeId));


            CreateMap<PersonProfileQueryResource, JceProfileQuery>();
            CreateMap<AdminProfileQueryResource, JceProfileQuery>();


            CreateMap(typeof(QueryResult<>), typeof(AdminProfileQueryResource));
            CreateMap(typeof(QueryResult<>), typeof(PersonProfileQueryResource));
            CreateMap(typeof(QueryResult<>), typeof(JceProfileQuery));


            CreateMap<ChildResource, Child>();
            CreateMap<ChildSaveResource, Child>();

            CreateMap<Child, ChildResource>();

        }
    }
}

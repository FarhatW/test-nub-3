using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.CE;

namespace jce.Common.Mapping
{
    public class CeMappingProfile : Profile
    {
        public CeMappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Ce, CeResource>()
                .ForMember(ceR => ceR.Address, opt => opt.MapFrom(ad => new AddressResource
                {
                    Address1 = ad.Address1,
                    Address2 = ad.Address2,
                    Company = ad.Company,
                    PostalCode = ad.PostalCode,
                    City = ad.City,
                    StreetNumber = ad.StreetNumber,
                    AddressExtra = ad.AddressExtra

                }))
                .ForMember(cer => cer.CatalogGoodsCount,
                    opt => opt.MapFrom(ce =>
                        (ce.Catalog.CatalogGoods != null && ce.Catalog.CatalogGoods.Count > 0)
                            ? ce.Catalog.CatalogGoods.Count
                            : 0))
                .ForMember(cer => cer.UserProfilesCount,
                    opt => opt.MapFrom(ce =>
                        (ce.UserProfiles != null && ce.UserProfiles.Count > 0) ? ce.UserProfiles.Count : 0));

            CreateMap<Ce, CeSaveResource>()
                .ForMember(ceR => ceR.Address, opt => opt.MapFrom(ad => new AddressResource
                {
                    Address1 = ad.Address1,
                    Address2 = ad.Address2,
                    Company = ad.Company,
                    PostalCode = ad.PostalCode,
                    City = ad.City,
                    StreetNumber = ad.StreetNumber,
                    AddressExtra = ad.AddressExtra
                }))
                .ForMember(c => c.UserProfiles, opt => opt.MapFrom(ce => ce.UserProfiles.Select(em => em.CeId)));
            
            //API Resource to Domaine


            CreateMap<CeSaveResource, Ce>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(c => c.Catalog, opt => opt.Ignore())
                .ForMember(c => c.CeSetup, opt => opt.Ignore())
                .ForMember(u => u.Address1, opt => opt.MapFrom(ur => ur.Address.Address1))
                .ForMember(u => u.Address2, opt => opt.MapFrom(ur => ur.Address.Address2))
                .ForMember(u => u.Company, opt => opt.MapFrom(ur => ur.Address.Company))
                .ForMember(u => u.PostalCode, opt => opt.MapFrom(ur => ur.Address.PostalCode))
                .ForMember(u => u.City, opt => opt.MapFrom(ur => ur.Address.City))
                .ForMember(u => u.StreetNumber, opt => opt.MapFrom(ur => ur.Address.StreetNumber));
        }
    }
}

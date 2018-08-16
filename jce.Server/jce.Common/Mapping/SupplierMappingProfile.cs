using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.Supplier;

namespace jce.Common.Mapping
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            //Domaine to API Resource

            CreateMap<Supplier, SupplierResource>()
                   .AfterMap((s, sr) =>
                   {
                       sr.ProductCount = s.Products.Count();
                   }); ;
            CreateMap<Supplier, SupplierSaveResource>();

            //API Resource to Domaine
            CreateMap<SupplierSaveResource, Supplier>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;

namespace jce.Common.Mapping
{
    public class GoodMappingProfile : Profile
    {
        public GoodMappingProfile()
        {
            CreateMap<Good, GoodResource>();

            CreateMap<Batch, BatchResource>();
            CreateMap<Product, ProductResource>();

            CreateMap<Good, GoodResource>()
                .Include<Batch, BatchResource>()
                .Include<Product, ProductResource>()
                .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id))
//                .ForMember(gr => gr.GoodDepartment, opt => opt.MapFrom(g => GoodDepartment.From(g.GoodDepartmentId) ))
//                .ForMember(gr => gr.ProductType, opt => opt.MapFrom(g => ProductType.From(g.ProductTypeId)))

                .AfterMap((g, gr) =>
                {
                    gr.IsBatch = g.GetType() == typeof(Batch);
                });

            CreateMap<GoodSaveResource, Good>()
                .Include<BatchSaveResource, Batch>()
                .Include<ProductSaveResource, Product>();


        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Resources;
using jce.Common.Resources.Product;

namespace jce.Common.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            //Domaine to API Resource

            CreateMap<Product, ProductResource>();

            CreateMap<Product, ProductSaveResource>();

            CreateMap<Product, ProductToBatchResource>();
                //.ForMember(gr => gr.ProductType, opt => opt.MapFrom(g => ProductType.From(g.ProductTypeId).Name));

            //API Resource to Domaine

            CreateMap<ProductSaveResource, Product>();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Product;

namespace jce.Common.Mapping
{
    public class BatchMappingProfile : Profile
    {
        public BatchMappingProfile()
        {

            //Domaine to API Resource

            CreateMap<Batch, BatchResource>();

            //API Resource to Domaine

            CreateMap<BatchSaveResource, Batch>()
                .ForMember(b => b.Id, opt => opt.Ignore())
                .ForMember(b => b.Products, opt => opt.Ignore())
                .AfterMap((br, b) =>
                {
                    var removedProducts = b.Products
                        .Where(bp => !br.Products.Any(pr => pr.RefPintel == bp.RefPintel)).ToList();

                    foreach (var item in removedProducts)
                    {
                        b.Products.Remove(item);
                        br.DeletedProds.Add(item.Id);
                    }

                    var addedProducts = br.Products
                        .Where(bp => !b.Products.Any(pr => pr.RefPintel == bp.RefPintel))
                        .Select(pr => new Product()
                        {
                            RefPintel = pr.RefPintel,
                            SupplierId = pr.SupplierId,
                            Details = pr.Details,
                            Title = pr.Title,
                            IsEnabled = pr.IsEnabled,
                            Price = pr.Price,
                            GoodDepartmentId = pr.GoodDepartmentId,
                            ProductTypeId =  pr.ProductTypeId,
                            IndexId = pr.IndexId,
                            IsDisplayedOnJCE = pr.IsDisplayedOnJCE,
                            IsBasicProduct = pr.IsBasicProduct,

                            CreatedBy = pr.CreatedBy,
                            CreatedOn = pr.CreatedOn,
                            UpdatedBy = pr.UpdatedBy,
                            UpdatedOn = pr.UpdatedOn,
                        })
                        .ToList();

                    foreach (var item in addedProducts)
                    {
                        b.Products.Add(item);
                    }
                });
        }

    }
}

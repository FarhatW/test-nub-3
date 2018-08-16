using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Catalog;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace jce.Common.Mapping
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            //Domaine to API Resource

            CreateMap<Catalog, CatalogResource>()
                .BeforeMap((c, cr) =>
                {
                    foreach (var item in c.CatalogGoods)
                    {
                        if (item.Good.GetType() == typeof(Batch))
                        {
                            var batch = (Batch)item.Good;

                            if (batch?.Products.Count > 0)
                            {
                                foreach (var prod in batch.Products)
                                {
                                    var productResourceToBatch = new ProductToBatchResource()
                                    {
                                        Id = prod.Id,
                                        Title = prod.Title,
                                        RefPintel = prod.RefPintel,
                                        Details = prod.Details,
                                        SupplierId = prod.SupplierId,

                                    };
                                }
                            }
                        }

                        var dynamicGood = ConvertToDynObj(item.Good);
                        dynamicGood.dateMin = item.DateMin;
                        dynamicGood.dateMax = item.DateMax;
                        dynamicGood.clientProductAlias = item.ClientProductAlias;
                        dynamicGood.employeeParticipationMessage = item.EmployeeParticipationMessage;
                        dynamicGood.catalogId = item.CatalogId;
                        dynamicGood.goodId = item.GoodId;
                        dynamicGood.isAddedManually = item.IsAddedManually;
                        dynamicGood.isBatch = item.Good.GetType() == typeof(Batch);

                        cr.CatalogGoods.Add(dynamicGood);

                    }
                })
                .ForMember(cr => cr.CatalogGoods, opt => opt.Ignore())
                .ForMember(cr => cr.ExpirationDate,
                    opt => opt.MapFrom(c => c.ExpirationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));



            CreateMap<Catalog, CatalogSaveResource>()
                .ForMember(cr => cr.CatalogGoods, opt => opt.MapFrom(c => c.CatalogGoods.Select(cp => cp.GoodId)));

            //API Resource to Domaine

            CreateMap<CatalogLettersSaveResource, Catalog>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CatalogGoods, opt => opt.Ignore())
                .AfterMap((cr, c) =>
                {
                    AfterMapCatalogGoods(c, cr);
                });

            CreateMap<CatalogSaveResource, Catalog>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CatalogGoods, opt => opt.Ignore())
                .AfterMap((cr, c) =>
                {
                    AfterMapCatalogGoods(c, cr);
                });
        }

        public static void AfterMapCatalogGoods(Catalog c, CatalogSaveResource cr)
        {
            var removedProducts = c.CatalogGoods
                .Where(cp => !cr.CatalogGoods.Any(pr => pr.GoodId == cp.GoodId)).ToList();

            foreach (var item in removedProducts)
            {
                c.CatalogGoods.Remove(item);
            }

            var addedProducts = cr.CatalogGoods.Where(cp => !c.CatalogGoods.Any(pr => pr.GoodId == cp.GoodId))
                .Select(pr => new CatalogGood
                {
                    GoodId = pr.GoodId,
                    ClientProductAlias = pr.ClientProductAlias,
                    CatalogId = pr.CatalogId,
                    DateMin = setDateMin(pr.DateMin),
                    DateMax = setDateMax(pr.DateMax),
                    EmployeeParticipationMessage = pr.EmployeeParticipationMessage,
                    IsAddedManually = pr.IsAddedManually ?? false,
                })
                .ToList();

            foreach (var item in addedProducts)
            {
                c.CatalogGoods.Add(item);
            }

            var editedProducts = c.CatalogGoods.Where(cp => cr.CatalogGoods.Any(
                pr => ((!String.IsNullOrEmpty(pr.ClientProductAlias) && !pr.ClientProductAlias.Equals(cp.ClientProductAlias)
                || (!String.IsNullOrEmpty(pr.EmployeeParticipationMessage) && !pr.EmployeeParticipationMessage.Equals(cp.EmployeeParticipationMessage))
                || setDateMin(pr.DateMin) != cp.DateMin ||
                        setDateMax(pr.DateMax) != cp.DateMax) && pr.GoodId == cp.GoodId
                      ))).ToList();

            foreach (CatalogGood item in editedProducts)
            {
                var newEditedProducts = cr.CatalogGoods.FirstOrDefault(
                    cp => ((!String.IsNullOrEmpty(cp.ClientProductAlias) && !cp.ClientProductAlias.Equals(item.ClientProductAlias)) ||
                           (!String.IsNullOrEmpty(cp.EmployeeParticipationMessage) && !cp.EmployeeParticipationMessage.Equals(item.EmployeeParticipationMessage)) ||
                           setDateMin(cp.DateMin) != item.DateMin || setDateMax(cp.DateMax) != item.DateMax)  &&
                          cp.GoodId == item.GoodId);

                item.ClientProductAlias = newEditedProducts.ClientProductAlias;
                item.DateMin = setDateMin(newEditedProducts.DateMin);
                item.DateMax = setDateMax(newEditedProducts.DateMax);
                item.IsAddedManually = newEditedProducts.IsAddedManually ?? false;
                item.EmployeeParticipationMessage = newEditedProducts.EmployeeParticipationMessage;
            }
        }

        public static DateTime setDateMin(int year)
        {
            var dateMin = new DateTime(year, 1, 1, 0, 0, 1);

            return dateMin;
        }
        public static DateTime setDateMax(int year)
        {
            var dateMax = new DateTime(year, 12, 31, 23, 59, 59);

            return dateMax;
        }

        public static dynamic ConvertToDynObj(object good, List<string> attributes = null)
        {
            var dynamicObject = new ExpandoObject() as IDictionary<string, object>;

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(good.GetType()))
            {
                if (attributes == null)
                {
                    if (property.GetValue(good) != null)
                    {
                        var propertyToLower = char.ToLower(property.Name[0]) + property.Name.Substring(1);
                        dynamicObject.Add(propertyToLower, property.GetValue(good));
                    }
                }
                else if (attributes.Contains(property.Name) == true)
                {
                    dynamicObject.Add(property.Name, property.GetValue(good));
                }
            }

            return dynamicObject;
        }

    }

}


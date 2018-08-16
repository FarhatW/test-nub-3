using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Product;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IMapper _mapper;
        public ISaveHistoryActionData SaveHistoryActionData { get; }
        public IRepository<JceDbContext> Repository { get; set; }
        public IUnitOfWork UnitOfWork { get; }


        public ProductManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
        {
            SaveHistoryActionData = saveHistoryActionData;

            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;
        }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public async Task SaveHistoryAction(string action, ResourceEntity userProfile)
        {
            var resource = new HistoryActionResource
            {
                ActionName = action,
                Content = JsonConvert.SerializeObject(userProfile),
                UserId = userProfile.UpdatedBy,

                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = userProfile.UpdatedBy,
                UpdatedBy = userProfile.UpdatedBy,
                TableName = "products"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task Delete(int id)
        {
            var product = await Repository.GetOne<Product>().FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("employee not found");
            }

            var resource =  _mapper.Map<Product, ProductResource> (product);
            Repository.Remove(product);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public bool ProductExist(string refPintel)
        {
            var products = Repository.GetAll<Product>();

            return products != null && products.Any(x => x.RefPintel == refPintel);
        }

        public async Task<QueryResult<ProductResource>> GetAll(FilterResource filteResource)
        {
            var queryResource = (ProductQueryResource)filteResource;
            var result = new QueryResult<Product>();
            var queryObj = _mapper.Map<ProductQueryResource, ProductQuery>(queryResource);

            var query = Repository.GetAll<Product>().AsQueryable();

            if (queryResource.PintelSheet.HasValue)
            {
                //query = query.Where(p => p.PintelSheetId == queryResource.PintelSheet);
            }
            else if(queryResource.RefPintelArray != null)
            {
                var refArray = queryResource.RefPintelArray.Split(',');
                refArray.Where(str => !String.IsNullOrEmpty(str));

                query = query.Where(p => refArray.Contains(p.RefPintel));
                queryObj.PageSize = Convert.ToByte(query.Count());

            }
            else if(queryResource.ProductLettersArray != null)
            {
                var productLettersArray = queryResource.ProductLettersArray.Split(',');
                productLettersArray.Where(str => !String.IsNullOrEmpty(str));

                //query = query.Where(p => productLettersArray.Contains(p.IndexID));
                queryObj.PageSize = Convert.ToByte(query.Count());
            }

            var columnMap = new Dictionary<string, Expression<Func<Product, object>>>
            {
                //["refSupplier"] = p => p.RefSupplier,
                //["isActif"] = p => p.IsActif,
                //["createdOn"] = p => p.CreatedOn,
            };

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Product>, QueryResult<ProductResource>>(result);
        }

        public async Task<ProductResource> Add(ResourceEntity resourceEntity)
        {
            var productSaveResource = (ProductSaveResource)resourceEntity;

            if (ProductExist(productSaveResource.RefPintel))
            {
                throw new Exception("Product already Found");
            }


            var product = _mapper.Map<ProductSaveResource, Product>(productSaveResource);

            Repository.Add(product);

            await SaveChanges();
            await SaveHistoryAction("Add", productSaveResource);

            return _mapper.Map<Product, ProductResource>(product);
        }

        public async Task<ProductResource> GetItemById(int id, FilterResource filterResource = null)
        {

            var queryFilterResource = (ProductQueryResource)filterResource;

            var product = await GetFilteredProduct(id, queryFilterResource);

            var result = _mapper.Map<Product, ProductResource>(product);

            return result;
        }

        public async Task<ProductResource> GetItemByRefPintel(string refPintel)
        {
            var product = await Repository.GetOne<Product>().FirstOrDefaultAsync(p => p.RefPintel == refPintel);

            var result = _mapper.Map<Product, ProductResource>(product);

            return result;
        }

        public async Task<ProductListResource> MultipleAdd(ProductSaveResource[] productSaveResourceArray)
        {
            var productResourceList = new ProductListResource();

            var goods = await Repository.GetAll<Good>().ToListAsync();

            for (int i = 0; i < productSaveResourceArray.Length; i++)
            {

                if (!ProductExists(productSaveResourceArray[i], goods))
                {
                    var product = _mapper.Map<ProductSaveResource, Product>(productSaveResourceArray[i]);

                    Repository.Add(product);

                    goods.Add(product);
                }
                else
                {
                    productResourceList.DuplicatedRefList.Add(productSaveResourceArray[i].RefPintel);
                }
               
            }

            await SaveChanges();
            //await SaveHistoryAction("Add", productSaveResourceArray);

            productResourceList.NotAddedProductCount = productResourceList.DuplicatedRefList.Count();
            productResourceList.AddedProductCount = productResourceList.Products.Count();

            return productResourceList;
        }

        public bool ProductExists(ProductSaveResource good, List<Good> goodList)
        {
            return goodList != null && goodList.Any(x => x.RefPintel == good.RefPintel);
        }

        public async Task<ProductResource> Update(int id, ResourceEntity resourceEntity)
        {
            var productSave = (ProductSaveResource)resourceEntity;


            var product = await Repository.GetOne<Product>().FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new Exception("product not found");
            }

            _mapper.Map(productSave, product);

            product.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", productSave);
            var result = await GetItemById(product.Id);

            return result;
        }

        public async Task<Product> GetFilteredProduct(int id, ProductQueryResource productQueryResource)
        {
            var product = new Product();

            if (productQueryResource != null)
            {
                if (productQueryResource.PintelSheet.HasValue)
                {
                    //product = await Repository.GetOne<Product>()
                    //    .FirstOrDefaultAsync(p => p.PintelSheetId == productQueryResource.PintelSheet);
                }
                else if (!String.IsNullOrEmpty(productQueryResource.RefPintel))
                {
                    product = await Repository.GetOne<Product>()
                        .FirstOrDefaultAsync(p => p.RefPintel == productQueryResource.RefPintel);
                }
            }
            else
            {
                product = await Repository.GetOne<Product>().FirstOrDefaultAsync(p => p.Id == id);
            }

            return product;
        }
    }
}

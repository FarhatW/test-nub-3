using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.Helpers;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class GoodManager : IGoodManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }
        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }
        private IRepository<JceDbContext> Repository { get; }

        public GoodManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task SaveHistoryAction(string action, ResourceEntity good)
        {
            var resource = new HistoryActionResource
            {
                ActionName = action,
                Content = JsonConvert.SerializeObject(good),
                UserId = good.UpdatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = good.UpdatedBy,
                UpdatedBy = good.UpdatedBy,
                TableName = "good"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task Delete(int id)
        {
            var good = await Repository.GetOne<Good>().FirstOrDefaultAsync(c => c.Id == id);

            if (good == null)
            {
                throw new Exception("good not Found");
            }
            var resource = _mapper.Map<Good, GoodResource>(good);

            Repository.Remove(good);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<QueryResult<GoodResource>> GetAll(FilterResource filteResource)
        {
            var queryFilterResource = (GoodQueryResource)filteResource;
            var result = await GetFilteredProducts(queryFilterResource);
            var retour = _mapper.Map<QueryResult<Good>, QueryResult<GoodResource>>(result);

            return retour;
        }

        public async Task<GoodResource> Add(ResourceEntity resourceEntity)
        {
            var goodSaveResource = (GoodSaveResource)resourceEntity;

            var good = _mapper.Map<GoodSaveResource, Good>(goodSaveResource);

            Repository.Add(good);

            await SaveChanges();

            await SaveHistoryAction("Add", goodSaveResource);

            good = await Repository.GetOne<Good>()
                .FirstOrDefaultAsync(b => b.Id == good.Id);

            if (good is Batch)
            {
                good = await Repository.GetOne<Batch>()
                    .Include(b => b.Products).FirstOrDefaultAsync(b => b.Id == good.Id);
            }

            return _mapper.Map<Good, GoodResource>(good);
        }

        public async Task<GoodResource> GetItemById(int id, FilterResource filteResource = null)
        {
            var queryFilterResource = (GoodQueryResource)filteResource;
            var good = await Repository
                .GetOne<Good>()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (good == null)
            {
                return null;
            }

            if (good.GetType() == typeof(Batch))
            {
                good = await Repository.GetOne<Batch>()
                    .Include(b => b.Products).FirstOrDefaultAsync(b => b.Id == id);
            }

            return _mapper.Map<Good, GoodResource>(good);
        }

        public async Task<GoodResource> GetItemByRefPintel(string refPintel)
        {
            var good = await Repository.GetOne<Good>().
                FirstOrDefaultAsync(p => p.RefPintel == refPintel);

            if (good != null)
            {
                if (good.GetType() == typeof(Batch))
                {
                    return _mapper.Map<Good, BatchResource>(good);
                }
                else
                {
                    return _mapper.Map<Good, ProductResource>(good);
                }
            }

            return _mapper.Map<Good, GoodResource>(good);
        }

        public async Task<GoodResource> Update(int id, ResourceEntity resourceEntity)
        {
            var good = await Repository.GetOne<Good>()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (good == null)
            {
                throw new Exception("good not found");
            }

            var goodSave = (GoodSaveResource)resourceEntity;

            _mapper.Map(goodSave, good);

            good.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", goodSave);

            var result = await GetItemById(good.Id);
            return result;
        }

        public async Task<QueryResult<Good>> GetFilteredProducts(GoodQueryResource goodQueryResource)
        {
            var batches = await Repository.GetAll<Good>().OfType<Batch>().Include(b => b.Products).ToListAsync();
            var products = await Repository.GetAll<Good>().OfType<Product>().ToListAsync();

            products = products.Where(p => batches.Any(b => b.Products.Any(g => g.Id == p.Id))).ToList();

            var query = Repository.GetAll<Good>().Where(g => products.All(p => p.Id != g.Id)).AsQueryable();

            var queryObj = _mapper.Map<GoodQueryResource, GoodQuery>(goodQueryResource);

            var columnMap = new Dictionary<string, Expression<Func<Good, object>>>
            {
                ["refPintel"] = g => g.RefPintel,
                ["id"] = g => g.Id,
                ["price"] = g => g.Price,
            };

            if (goodQueryResource.RefPintelArray != null)
            {
                var refArray = goodQueryResource.RefPintelArray.Replace(" ", String.Empty).Split(',');
                refArray.Where(str => !String.IsNullOrEmpty(str));

                query = query.Where(g => refArray.Contains(g.RefPintel));
            }

            else if (!String.IsNullOrEmpty(goodQueryResource.ProductIndex))
            {
                var refArray = goodQueryResource.ProductIndex.Split(',');
                refArray.Where(str => !String.IsNullOrEmpty(str));

                query = query.Where(p => goodQueryResource.ProductIndex.Contains(p.IndexId));
            }

            else if (!string.IsNullOrEmpty(goodQueryResource.Search))
            {
                query = query.Where(p => p.RefPintel.ToLowerInvariant().Contains(goodQueryResource.Search.ToLowerInvariant())
                    || (!string.IsNullOrEmpty(p.Title) && p.Title.ToLowerInvariant().Contains(goodQueryResource.Search.ToLowerInvariant())));

                if (goodQueryResource.IsBatch.HasValue && !goodQueryResource.IsBatch.Value)
                {
                    query = query.Except(query.Where(b => b is Batch));
                }
            }

            else if (!string.IsNullOrEmpty(goodQueryResource.PintelSheetArray))
            {
                var pintelSheetArray = goodQueryResource.PintelSheetArray.Split(',');
                pintelSheetArray.Where(str => !String.IsNullOrEmpty(str));

                var prodList = new List<Good>();

                foreach (var item in query)
                {
                    if (item.GetType() != typeof(Product)) continue;
                    var pr = item as Product;
                    if (pintelSheetArray.Contains(pr.PintelSheetId.ToString()))
                    {
                        prodList.Add(item);
                    }
                }

                query = query.Where(p => prodList.Any(pr => pr.Id == p.Id));
            }

            queryObj.PageSize = ManagersHelper.CheckPageSize(goodQueryResource, query);

            var result = new QueryResult<Good> {TotalItems = await query.CountAsync()};

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}

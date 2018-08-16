using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.Core;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Catalog;
using jce.Common.Resources.Good;
using jce.Common.Resources.PintelSheet;
using jce.Common.Resources.Product;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class CatalogManager : ICatalogManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }

        private IGoodManager GoodManager { get; }
        private IPintelSheetManager PintelSheetManager { get; }

        private IRepository<JceDbContext> Repository { get; }

        public CatalogManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper, IGoodManager goodManager, IPintelSheetManager pintelSheetManager)
        {
            SaveHistoryActionData = saveHistoryActionData;
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;
            GoodManager = goodManager;
            PintelSheetManager = pintelSheetManager;
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
                TableName = "catalog"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public bool CatalogExist(int CeID)
        {
            var catalogs = Repository.GetAll<Catalog>();

            return catalogs != null && catalogs.Any(x => x.CeId == CeID);
        }

        public async Task Delete(int id)
        {
            var catalog = await Repository.GetOne<Catalog>().FirstOrDefaultAsync(c => c.Id == id);

            if (catalog == null)
            {
                throw new Exception("catalog not Found");
            }
            var resource = _mapper.Map<Catalog, CatalogResource>(catalog);

            Repository.Remove(catalog);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<QueryResult<CatalogResource>> GetAll(FilterResource filteResource)
        {
            var queryFilterResource = (CatalogQueryResource)filteResource;
            var result = new QueryResult<Catalog>();
            var queryObj = _mapper.Map<CatalogQueryResource, CatalogQuery>(queryFilterResource);
            var filters = queryObj;

            var query = Repository.GetAll<Catalog>()
                .Include(c => c.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .AsQueryable();

            var columnMap = new Dictionary<string, Expression<Func<Catalog, object>>>
            {
                //["clientRef"] = c => c.ClientRef,
            };

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Catalog>, QueryResult<CatalogResource>>(result);
        }

        public async Task<CatalogResource> Add(ResourceEntity resourceEntity)
        {
            var catalogSaveResource = (CatalogSaveResource)resourceEntity;

            if (CatalogExist(catalogSaveResource.CeId))
            {
                throw new Exception("Catalog already found");
            }

            var catalog = _mapper.Map<CatalogSaveResource, Catalog>(catalogSaveResource);

            Repository.Add(catalog);


            await SaveChanges();

            await SaveHistoryAction("Add", catalogSaveResource);

            catalog = await Repository.GetOne<Catalog>().Include(c => c.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .FirstOrDefaultAsync(c => c.Id == catalog.Id);

            return _mapper.Map<Catalog, CatalogResource>(catalog);
        }

        public async Task<CatalogResource> GetItemById(int id, FilterResource filteResource = null)
        {
            var queryFilterResource = (CatalogQueryResource)filteResource;

            var catalog = await GetFilteredCatalog(id, queryFilterResource);
           
            var result = _mapper.Map<Catalog, CatalogResource>(catalog);

            return result;
        }

        public async Task<CatalogResource> UpdatePintelSheets(int id, ResourceEntity catalogResource,
            List<PintelSheetResource> ressources)
        {
            var catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (catalog == null)
            {
                throw new Exception("catalog not found");
            }

            var catalogSave = (CatalogSaveResource)catalogResource;

            foreach (var item in ressources)
            {

                foreach (var prod in item.Products)
                {
                    var cpr = new GoodToCatalogSaveResource()
                    {
                        CatalogId = id,
                        GoodId = prod.Id,
                        ClientProductAlias = "",
                        DateMin = AgeGroup.SetDateToInt(item.AgeGroup.DateMin),
                        DateMax = AgeGroup.SetDateToInt(item.AgeGroup.DateMax),
                        IsAddedManually = false,
                    };
                    catalogSave.CatalogGoods.Add(cpr);
                }
            }

            _mapper.Map(catalogSave, catalog);

            catalog.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", catalogSave);

            var result = await GetItemById(catalog.Id);
            return result;
        }

        public async Task<CatalogResource> UpdateLetters(int id, ResourceEntity catalogResource,
            List<GoodResource> ressources)
        {
            var catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (catalog == null)
            {
                throw new Exception("catalog not found");
            }

            var catalogSave = (CatalogLettersSaveResource)catalogResource;

            foreach (var item in ressources)
            {
                    var cpr = new GoodToCatalogSaveResource()
                    {
                        CatalogId = id,
                        GoodId = item.Id,
                        ClientProductAlias = "",
                        DateMin = catalogSave.ProductLetters.FirstOrDefault(g => g.Letter == item.IndexId).DateMin,
                        DateMax = catalogSave.ProductLetters.FirstOrDefault(g => g.Letter == item.IndexId).DateMax,
                        IsAddedManually = false,
                    };
                    catalogSave.CatalogGoods.Add(cpr);
                }

            _mapper.Map(catalogSave, catalog);

            catalog.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", catalogSave);

            var result = await GetItemById(catalog.Id);
            return result;
        }

        public async Task<CatalogResource> Update(int id, ResourceEntity resourceEntity)
        {
            var catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .FirstOrDefaultAsync(c => c.Id == id);


            if (catalog == null)
            {
                throw new Exception("catalog not found");
            }

            var catalogSave = (CatalogSaveResource)resourceEntity;

            if (catalogSave.CatalogType != catalog.CatalogType)
            {
                catalog.CatalogGoods.Clear();
                catalogSave.CatalogGoods.Clear();
            }

            _mapper.Map(catalogSave, catalog);

            catalog.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", catalogSave);

            var result = await GetItemById(catalog.Id);
            return result;
        }

        public async Task<Catalog> GetFilteredCatalog(int id, CatalogQueryResource catalogQueryResource)
        {
            var catalog = new Catalog();

            if (catalogQueryResource != null)
            {
                var filters = _mapper.Map<CatalogQueryResource, CatalogQuery>(catalogQueryResource);
                var queryObj = filters;

                if (filters.CeId.HasValue)
                {
                    catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                        .ThenInclude(cp => cp.Good).FirstOrDefaultAsync(c => c.CeId == id);
                }

                else if (!String.IsNullOrEmpty(filters.DateMin) && !String.IsNullOrEmpty(filters.DateMax))
                {
                    DateTime dMin = new DateTime(Convert.ToInt32(filters.DateMin), 1, 1);
                    DateTime dMax = new DateTime(Convert.ToInt32(filters.DateMax), 1, 1);
                    catalog.CatalogGoods = catalog.CatalogGoods
                        .Where(p => p.DateMin.Year >= dMin.Year && p.DateMax.Year <= dMax.Year).ToList();
                }

                else if (!String.IsNullOrEmpty(filters.DateMax))
                {
                    DateTime dMax = new DateTime(Convert.ToInt32(filters.DateMax), 1, 1);

                    catalog.CatalogGoods = catalog.CatalogGoods
                        .Where(p => p.DateMin.Year <= dMax.Year).ToList();
                }
                else if (!String.IsNullOrEmpty(filters.DateMin))
                {
                    DateTime dMin = new DateTime(Convert.ToInt32(filters.DateMin), 1, 1);

                    catalog.CatalogGoods = catalog.CatalogGoods
                        .Where(p => p.DateMin.Year >= dMin.Year).ToList();
                }
                else if (!String.IsNullOrEmpty(filters.BirthDate))
                {
                    DateTime bDayDate = new DateTime(Convert.ToInt32(filters.BirthDate), 1, 1);

                    catalog.CatalogGoods = catalog.CatalogGoods
                        .Where(p => p.DateMin.Year <= bDayDate.Year && p.DateMax.Year >= bDayDate.Year).ToList();
                }
                else
                {
                    catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                        .ThenInclude(cp => cp.Good)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (catalog?.CatalogGoods.Count > 0)
                    {
                        foreach (var item in catalog.CatalogGoods)
                        {
                            if (item.Good.GetType() == typeof(Batch))
                            {
                                var batch = await GoodManager.GetItemById(item.GoodId);
                            }
                        }
                    }
                }
            }
            else
            {
                catalog = await Repository.GetOne<Catalog>().Include(p => p.CatalogGoods)
                    .ThenInclude(cp => cp.Good)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (catalog?.CatalogGoods.Count > 0)
                {
                    foreach (var item in catalog.CatalogGoods)
                    {
                        if (item.Good.GetType() == typeof(Batch))
                        {
                            var batch = await GoodManager.GetItemById(item.GoodId);
                        }
                    }
                }
            }

            return catalog;
        }
    }
}

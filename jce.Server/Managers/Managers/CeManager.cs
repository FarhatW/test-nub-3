using System;
using System.Collections.Generic;
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
using jce.Common.Resources.CE;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class CeManager : ICeManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }


        private IRepository<JceDbContext> Repository { get; }

        public CeManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork,
            IMapper mapper)
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
                TableName = "ces"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public bool CeExist(int id)
        {
            var ce = Repository.GetAll<Ce>();

            return ce != null && ce.Any(x => x.Id == id);
        }

        public async Task Delete(int id)
        {
            var ce = await Repository.GetOne<Ce>().FirstOrDefaultAsync(c => c.Id == id);

            if (ce == null)
            {
                throw new Exception("ce not Found");
            }

            var resource = _mapper.Map<Ce, CeResource>(ce);

            Repository.Remove(ce);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<QueryResult<CeResource>> GetAll(FilterResource filterResource)
        {
            var queryFilterResource = (CeQueryResource)filterResource;

            var result = new QueryResult<Ce>();
            var queryObj = _mapper.Map<CeQueryResource, CeQuery>(queryFilterResource);
            var query = await GetFilteredCEs(queryFilterResource);

            var columnMap = new Dictionary<string, Expression<Func<Ce, object>>>
            {
                //["clientRef"] = c => c.ClientRef,
                ["id"] = c => c.Id,
                ["address.company"] = c => c.Company,
                ["address.postalCode"] = c => c.PostalCode
            };

            result.TotalItems = await query.CountAsync();

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<Ce>, QueryResult<CeResource>>(result);
        }

        public async Task<CeResource> Add(ResourceEntity resourceEntity)
        {
            var ceSaveResource = (CeSaveResource)resourceEntity;

            if (CeExist(ceSaveResource.Id))
            {
                throw new Exception("CE already found");
            }

            var ce = _mapper.Map<CeSaveResource, Ce>(ceSaveResource);

            ce.CreatedOn = DateTime.Now;

            Repository.Add(ce);

            await SaveChanges();

            ce = await Repository.GetOne<Ce>()
                .Include(c => c.CeSetup).FirstOrDefaultAsync(c => c.Id == ce.Id);

            return _mapper.Map<Ce, CeResource>(ce);
        }

        public async Task<CeResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var queryFilterResource = (CeQueryResource)filterResource;
            var ce = await GetFilteredCE(id, queryFilterResource);

            if(ce == null)
            {
                return null;
            }

            var catalog = _mapper.Map<Catalog, CatalogResource>(ce.Catalog);
            var result = _mapper.Map<Ce, CeResource>(ce);
            result.Catalog = catalog;

            return result;
        }

        public async Task<CeResource> Update(int id, ResourceEntity resourceEntity)
        {
            var ce = await Repository.GetOne<Ce>()
                .Include(c => c.Catalog)
                .Include(em => em.UserProfiles)
                .Include(cs => cs.CeSetup)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (ce == null)
            {
                throw new Exception("ce not found");
            }

            var ceSave = (CeSaveResource)resourceEntity;

            _mapper.Map(ceSave, ce);

            ce.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", ceSave);

            var updatedCe = await Repository.GetOne<Ce>()
                .Include(_ce => _ce.CeSetup)
                .Include(_ce => _ce.Catalog)
                .ThenInclude(c => c.CatalogGoods)
                .ThenInclude(cp => cp.Good)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<Ce, CeResource>(updatedCe);

            return result;
        }


        public async Task<Ce> GetFilteredCE(int id, CeQueryResource ceQueryResource)
        {
            Ce ce = null;

            if (ceQueryResource.CeSetupQuery.HasValue)
            {
                ce = await Repository.GetOne<Ce>()
                    .Include(_ce => _ce.CeSetup).FirstOrDefaultAsync(c => c.Id == id);
            }
            else if (ceQueryResource.CatalogQuery.HasValue)
            {
                ce = await Repository.GetOne<Ce>()
                    .Include(_ce => _ce.Catalog)
                    .ThenInclude(c => c.CatalogGoods)
                    .ThenInclude(p => p.Good)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            else if (ceQueryResource.CeInfosQuery.HasValue)
            {
                ce = await Repository.GetOne<Ce>()
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            else if (ceQueryResource.CeUserProfilesQuery.HasValue)
            {
                ce = await Repository.GetOne<Ce>()
                    .Include(_ce => _ce.UserProfiles)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }

            else
            {
                ce = await Repository.GetOne<Ce>()
                    //.Include(_ce => _ce.UserProfiles)
                    //.ThenInclude(up => up.Children)
                    .Include(_ce => _ce.CeSetup)
                    .Include(_ce => _ce.Catalog)
                    .ThenInclude(c => c.CatalogGoods)
                    .ThenInclude(cp => cp.Good)
                    //.Include(_ce => _ce.Events)
                    //.ThenInclude(ev => ev.Schedules)
                    //.ThenInclude(sch => sch.EventSchedulesEmployees)
                    //.ThenInclude(see => see.Employee)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            return ce;
        }

        public async Task<IQueryable<Ce>> GetFilteredCEs(CeQueryResource ceQueryResource)
        {

            var ces = Enumerable.Empty<Ce>().AsQueryable();

            if (ceQueryResource.CeSetupQuery.HasValue)
            {
                ces = Repository.GetAll<Ce>()
                    .Include(_ce => _ce.CeSetup);
            }
            else if (ceQueryResource.CatalogQuery.HasValue)
            {
                ces = Repository.GetAll<Ce>()
                    .Include(_ce => _ce.Catalog).ThenInclude(c => c.CatalogGoods)
                    .ThenInclude(p => p.Good);
            }
            else if (ceQueryResource.CeInfosQuery.HasValue)
            {
                ces = Repository.GetAll<Ce>();
            }
            else if (ceQueryResource.CeUserProfilesQuery.HasValue)
            {
                ces = Repository.GetAll<Ce>()
                    .Include(_ce => _ce.UserProfiles);
            }

            else if (ceQueryResource.IdAdmin.HasValue)
            {
                ces = Repository.GetAll<Ce>().Where(ce => ce.AdminJceProfileId == ceQueryResource.IdAdmin.Value);

            }

            else if (!String.IsNullOrEmpty(ceQueryResource.Search))
            {
                ces = Repository.GetAll<Ce>().Where(ce => ce.Name.ToLowerInvariant().Contains(ceQueryResource.Search.ToLowerInvariant())
                 || (!string.IsNullOrEmpty(ce.Company) && ce.Company.ToLowerInvariant().Contains(ceQueryResource.Search.ToLowerInvariant()))
                 || (!string.IsNullOrEmpty(ce.City) && ce.City.ToLowerInvariant().Contains(ceQueryResource.Search.ToLowerInvariant()))
                 );
            }


            else
            {
                ces = Repository.GetAll<Ce>()
                    //.Include(_ce => _ce.UserProfiles)
                    //.ThenInclude(up => up.Children)
                    .Include(_ce => _ce.CeSetup)
                    .Include(_ce => _ce.Catalog)
                    .ThenInclude(c => c.CatalogGoods)
                    .ThenInclude(cp => cp.Good)
                    //.Include(_ce => _ce.Events)
                    //.ThenInclude(ev => ev.Schedules)
                    //.ThenInclude(sch => sch.EventSchedulesEmployees)
                    //.ThenInclude(see => see.Employee)
                    ;
            }
            return ces;
        }

    }
}
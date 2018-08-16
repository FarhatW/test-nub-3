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
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.CeSetup;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class CeSetupManager : ICeSetupManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }

        private IRepository<JceDbContext> Repository { get; }

        public CeSetupManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                TableName = "CeSetup"

            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public bool CeSetupExist(int CeID)
        {
            var ceSetups = Repository.GetAll<CeSetup>();

            return ceSetups != null && ceSetups.Any(x => x.CeId == CeID);
        }

        public async Task<QueryResult<CeSetupResource>> GetAll(FilterResource filterResource)
        {
            var queryFilterResource = (CeSetupQueryResource) filterResource;
            var result = new QueryResult<CeSetup>();

            var queryObj = _mapper.Map<CeSetupQueryResource, CeSetupQuery>(queryFilterResource);
            var filters = queryObj;

            var query = Repository.GetAll<CeSetup>();


            if (filterResource != null)
            {
                 filters = _mapper.Map<CeSetupQueryResource, CeSetupQuery>(queryFilterResource);

                if (!String.IsNullOrEmpty(filters.CeId))
                {
                    query =  query.Where(c => c.CeId == Int32.Parse(filters.CeId));
                }
            }


            var columnMap = new Dictionary<string, Expression<Func<CeSetup, object>>>()
            {

            };
            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();
            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<CeSetup>, QueryResult<CeSetupResource>>(result);

        }

        public async Task<CeSetupResource> Add(ResourceEntity resourceEntity)
        {
            var ceSetupSaveResource = (CeSetupSaveResource) resourceEntity;

            if (CeSetupExist(ceSetupSaveResource.CeId))
            {
                throw new Exception("CeSetup already found");
            }

            var ceSetup = _mapper.Map<CeSetupSaveResource, CeSetup>(ceSetupSaveResource);

            Repository.Add(ceSetup);

            await SaveChanges();
            await SaveHistoryAction("Add", ceSetupSaveResource);

            ceSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(c => c.Id == ceSetup.Id);

            return _mapper.Map<CeSetup, CeSetupResource>(ceSetup);
        }

        public async Task<CeSetupResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var queryFilterResource = (CeSetupQueryResource) filterResource;

            var ceSetup = new CeSetup();

             ceSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<CeSetup, CeSetupResource>(ceSetup);

            return result;
        }

        public async Task<CeSetupResource> Update(int id, ResourceEntity resourceEntity)
        {
            var ceSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(c => c.Id == id);
            var ceSetupSaveResource = (CeSetupSaveResource) resourceEntity;

            if (ceSetup == null)
            {
                throw new Exception("cesetup not found");
            }

            _mapper.Map(ceSetupSaveResource, ceSetup);

            ceSetup.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("update", ceSetupSaveResource);

            var result = await GetItemById(ceSetup.Id);

            return result;

        }

        public async Task Delete(int id)
        {
            var ceSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(c => c.Id == id);

            if (ceSetup == null)
            {
                throw new Exception("CeSetup not found");
            }

            var ceSetupResource = _mapper.Map<CeSetup, CeSetupResource>(ceSetup);

            Repository.Remove(ceSetup);

            await SaveChanges();
            await SaveHistoryAction("Delete", ceSetupResource);
        }

        public async Task<CeSetupResource> GetByCeId(int ceId)
        {
            var ceSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(c => c.CeId == ceId);

            if (ceSetup == null)
            {
                throw new Exception("cesetup not found");
            }

            var result = _mapper.Map<CeSetup, CeSetupResource>(ceSetup);
            return result;
        } 
    }
}

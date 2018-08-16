using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;

namespace Managers
{
  public  class HistoryActionManager : IHistoryActionManager
    {

        private readonly IMapper _mapper;
        public ISaveHistoryActionData SaveHistoryActionData { get; }
        private IRepository<JceDbContext> Repository { get; }

        public HistoryActionManager(IRepository<JceDbContext> repository, IUnitOfWork saveData, IMapper mapper)
        {
            _mapper = mapper;
            Repository = repository;
            UnitOfWork = saveData;
        }

        public IUnitOfWork UnitOfWork { get; }


        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public Task Delete(int id)
        {
            throw new Exception("Cannot delete history");
        }

        public Task SaveHistoryAction(string action, ResourceEntity userProfile)
        {
            throw new NotImplementedException();
        }


        public async Task<HistoryActionResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var child = await Repository.GetOne<HistoryAction>().FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<HistoryAction, HistoryActionResource>(child);
        }

        public async Task<HistoryActionResource> Add(ResourceEntity resourceEntity)
        {
            var saveAction = (HistoryActionResource)resourceEntity;

            var child = _mapper.Map<HistoryActionResource, HistoryAction>(saveAction);

            Repository.Add(child);
            await SaveChanges();

            return _mapper.Map<HistoryAction, HistoryActionResource>(child);
        }

      
        public Task<HistoryActionResource> Update(int id, ResourceEntity resourceEntity)
        {
            throw new Exception("Cannot update history");
        }

        public async Task<QueryResult<HistoryActionResource>> GetAll(FilterResource filterResource)
        {
            var queryResource = (HistoryActionQueryResource)filterResource;

            var result = new QueryResult<HistoryAction>();

            var filters = _mapper.Map<HistoryActionQueryResource, HistoryActionQuery>(queryResource);

            var query = Repository.GetAll<HistoryAction>().AsQueryable();

            if (filters.UserId.HasValue)
            {
                query = query.Where(h => h.UserId == queryResource.UserId);
            }
            if (!string.IsNullOrEmpty(filters.ActionName))
            {
                query = query.Where(h => h.ActionName == queryResource.ActionName);
            }
            if (!string.IsNullOrEmpty(filters.TableName))
            {
                query = query.Where(h => h.TableName == queryResource.TableName);
            }
            if (!string.IsNullOrEmpty(filters.Date))
            {
                query = query.Where(h => h.CreatedOn.ToString(CultureInfo.InvariantCulture) == queryResource.Date);
            }

            var columMap = new Dictionary<string, Expression<Func<HistoryAction, object>>>
            {
                ["id"] = v => v.Id,
                ["userId"] = v => v.UserId,
                ["actionName"] = v => v.ActionName,
                ["createdOn"] = v => v.CreatedOn,
                ["tableName"] = v => v.CreatedOn,

            };

            var queryObj = _mapper.Map<HistoryActionQueryResource, HistoryActionQuery>(queryResource);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyOrdering(queryObj, columMap);

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<HistoryAction>, QueryResult<HistoryActionResource>>(result);

        }
    }
}

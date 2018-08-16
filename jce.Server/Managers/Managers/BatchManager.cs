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
using jce.Common.Resources.Batch;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class BatchManager: IBatchManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }

        private IRepository<JceDbContext> Repository { get; }

        public BatchManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                TableName = "batch"

            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task Delete(int id)
        {
            var batch = await Repository.GetOne<Batch>().FirstOrDefaultAsync(c => c.Id == id);

            if (batch == null)
            {
                throw new Exception("catalog not Found");
            }
            var resource = _mapper.Map<Batch, BatchResource>(batch);

            Repository.Remove(batch);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<QueryResult<BatchResource>> GetAll(FilterResource filteResource)
        {
            var queryFilterResource = (BatchQueryResource)filteResource;
            var result = new QueryResult<Batch>();
            var queryObj = _mapper.Map<BatchQueryResource, BatchQuery>(queryFilterResource);


            var filters = queryObj;

            var query = Repository.GetAll<Batch>().AsQueryable();
                //.Include(b => b.BatchProducts)
                //.ThenInclude(bp => bp.Product).AsQueryable();

            var columnMap = new Dictionary<string, Expression<Func<Batch, object>>>
            {
                //["clientRef"] = c => c.ClientRef,
            };

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Batch>, QueryResult<BatchResource>>(result);
        }

        public async Task<BatchResource> Add(ResourceEntity resourceEntity)
        {
            var batchSaveResource = (BatchSaveResource)resourceEntity;
            var batch = _mapper.Map<BatchSaveResource, Batch>(batchSaveResource);
            var goods = Repository.GetAll<Good>().ToList();


            if (BatchExists(batchSaveResource, goods))
            {
                throw new Exception("BATCH already found");
            }

            Repository.Add(batch);

            await SaveChanges();

            await SaveHistoryAction("Add", batchSaveResource);

            batch = await Repository.GetOne<Batch>()
                .Include(bp => bp.Products)
                .FirstOrDefaultAsync(b => b.Id == batch.Id);

            return _mapper.Map<Batch, BatchResource>(batch);
        }

        public async Task<BatchResource> GetItemById(int id, FilterResource filteResource = null)
        {
            var queryFilterResource = (BatchQueryResource)filteResource;

            var batch = new Batch();

                batch = await Repository.GetOne<Batch>()
                    .Include(b => b.Products)
                    .FirstOrDefaultAsync(b => b.Id == id);

            var result = _mapper.Map<Batch, BatchResource>(batch);

            return result;
        }

        public async Task<BatchResource> Update(int id, ResourceEntity resourceEntity)
        {
            var batch = await Repository.GetOne<Batch>()
                .Include(b => b.Products)
                //.ThenInclude(bp => bp.G)
                .FirstOrDefaultAsync(b => b.Id == id);

            var batchSave = (BatchSaveResource)resourceEntity;

            if (batch == null)
            {
                throw new Exception("batch not found");
            }

            _mapper.Map(batchSave, batch);

            batch.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", batchSave);

            var result = await GetItemById(batch.Id);

            result.DeletedProds = batchSave.DeletedProds;

            return result;
        }

        public async Task<BatchListResource> MultipleAdd(BatchSaveResource[] batchSaveResourceArray)
        {

            var goods = Repository.GetAll<Good>().ToList();
            var batchListResource = new BatchListResource();
            for (int i = 0; i < batchSaveResourceArray.Length; i++)
            {
                if (!BatchExists(batchSaveResourceArray[i], goods))
                {
                    var item = await Add(batchSaveResourceArray[i]);
                    batchListResource.Batches.Add(item);
                }
            }

            return batchListResource;
        }

        public bool BatchExists(BatchSaveResource batch, List<Good> goods)
        {
            return goods != null && goods.Any(x => x.RefPintel == batch.RefPintel);
        }




    }
}

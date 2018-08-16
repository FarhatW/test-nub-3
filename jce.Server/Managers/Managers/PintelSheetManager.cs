using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.Helpers;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.CE;
using jce.Common.Resources.PintelSheet;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class PintelSheetManager : IPintelSheetManager
    {
        private readonly IMapper _mapper;
        public ISaveHistoryActionData SaveHistoryActionData { get; }
        private IRepository<JceDbContext> Repository { get; }

        public PintelSheetManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
        {
            SaveHistoryActionData = saveHistoryActionData;

            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;

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
                TableName = "pintelSheet"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public async Task Delete(int id)
        {
            var pintelSheet = await Repository.GetOne<PintelSheet>().FirstOrDefaultAsync(c => c.Id == id);

            if (pintelSheet == null)
            {
                throw new Exception("pintelSheet not Found");
            }
            var resource = _mapper.Map<PintelSheet, PintelSheetResource>(pintelSheet);
            Repository.Remove(pintelSheet);
            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public bool PintelSheetExists(string SheetId)
        {
            var pintelSheets = Repository.GetAll<PintelSheet>();
            return pintelSheets != null && pintelSheets.Any(x => x.SheetId == SheetId);
        }

        public async Task<QueryResult<PintelSheetResource>> GetAll(FilterResource filteResource)
        {
            var queryFilterResource = (PintelQueryResource)filteResource;
            var result = await GetFilteredPintelSheets(queryFilterResource);

            return _mapper.Map<QueryResult<PintelSheet>, QueryResult<PintelSheetResource>>(result);
        }

        public async Task<PintelSheetResource> Add(ResourceEntity resourceEntity)
        {
            var pintelSheetSaveResource = (PintelSheetSaveResource)resourceEntity;

            if (PintelSheetExists(pintelSheetSaveResource.SheetId))
            {
                throw new Exception("PintelSheet already found");
            }

            var pintelSheet = _mapper.Map<PintelSheetSaveResource, PintelSheet>(pintelSheetSaveResource);

            Repository.Add(pintelSheet);

            await SaveChanges();
            await SaveHistoryAction("Add", pintelSheetSaveResource);

            pintelSheet = await Repository.GetOne<PintelSheet>().Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == pintelSheet.Id);

            return _mapper.Map<PintelSheet, PintelSheetResource>(pintelSheet);
        }

        public async Task<PintelSheetResource> Update(int id, ResourceEntity resourceEntity)
        {

            var pintelSheetSave = (PintelSheetSaveResource)resourceEntity;
            

            var pintelSheet = await Repository.GetOne<PintelSheet>().Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pintelSheet == null)
                throw new Exception("pintelSheet not Found");

         
            pintelSheet = new PintelSheet();
            _mapper.Map(pintelSheetSave, pintelSheet);

            pintelSheet.UpdatedOn = DateTime.Now;

            await SaveChanges();
            //await SaveHistoryAction("Update", pintelSheetSave);

            var result = await GetItemById(pintelSheet.Id);
            return result;
        }


        public async Task<PintelSheetResource> GetItemById(int id, FilterResource filteResource = null)
        {
            var queryFilterResource = (PintelQueryResource)filteResource;

            var pintelSheet = await Repository.GetOne<PintelSheet>().Include(ps => ps.Products)
                .FirstOrDefaultAsync(p => p.Id == id);

            var result = _mapper.Map<PintelSheet, PintelSheetResource>(pintelSheet);

            return result;
        }

        public async Task<QueryResult<PintelSheet>> GetFilteredPintelSheets(PintelQueryResource pintelQueryResource)
        {
            IQueryable<PintelSheet> query = Enumerable.Empty<PintelSheet>().AsQueryable();

            var queryObj = _mapper.Map<PintelQueryResource, PintelSheetQuery>(pintelQueryResource);

            if (!String.IsNullOrEmpty(pintelQueryResource.PintelSheetArray))
            {
                var pintelSheetArray = pintelQueryResource.PintelSheetArray.Split(',');
                pintelSheetArray.Where(str => !String.IsNullOrEmpty(str));

                query = Repository.GetAll<PintelSheet>()
                    .Include(ps => ps.Products).Where(ps => pintelSheetArray.Contains(ps.Id.ToString()))
                    .AsQueryable();

                queryObj.PageSize = Convert.ToByte(query.Count());
            }

            else if (pintelQueryResource.ReturnProducts.HasValue)
            {
                if (!pintelQueryResource.ReturnProducts.Value)
                {
                    query = Repository.GetAll<PintelSheet>()
                        .AsQueryable();

                    queryObj.PageSize = Convert.ToByte(query.Count());

                }
            }


            else
            {
                query = Repository.GetAll<PintelSheet>()
                    .Include(ps => ps.Products)
                    .AsQueryable();
            }

            var columnMap = new Dictionary<string, Expression<Func<PintelSheet, object>>>
            {
                ["ageGroup"] = p => p.AgeGroup
            };

            var result = new QueryResult<PintelSheet>();

            result.TotalItems = await query.CountAsync();

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }

    }
}

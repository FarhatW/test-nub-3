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
using jce.Common.Resources.Supplier;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class SupplierManager : ISupplierManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }
        public IUnitOfWork UnitOfWork { get; }
        private IRepository<JceDbContext> Repository { get; }
        private readonly IMapper _mapper;


        public SupplierManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData,
            IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task SaveHistoryAction(string action, ResourceEntity Supplier)
        {
            var resource = new HistoryActionResource
            {
                ActionName = action,
                Content = JsonConvert.SerializeObject(Supplier),
                UserId = Supplier.CreatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = Supplier.CreatedBy,
                UpdatedBy = Supplier.CreatedBy,
                TableName = "Suppliers"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task<SupplierResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var Supplier = await Repository.GetOne<Supplier>()
                .FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<Supplier, SupplierResource>(Supplier);
        }

        public async Task Delete(int id)
        {
            var supplier = await Repository.GetOne<Supplier>().FirstOrDefaultAsync(c => c.Id == id);

            if (supplier == null)
            {
                throw new Exception("catalog not Found");
            }
            var resource = _mapper.Map<Supplier, SupplierResource>(supplier);

            Repository.Remove(supplier);

            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<QueryResult<SupplierResource>> GetAll(FilterResource filteResource)
        {
            var queryFilterResource = (SupplierQueryResource)filteResource;
            var result = new QueryResult<Supplier>();
            var queryObj = _mapper.Map<SupplierQueryResource, SupplierQuery>(queryFilterResource);
            var filters = queryObj;

            var query = Repository.GetAll<Supplier>()
                .Include(c => c.Products)
                .AsQueryable();

            if (queryFilterResource.IsForDropDown.HasValue)
            {
                if (queryFilterResource.IsForDropDown.Value)
                {
                    queryObj.PageSize = Convert.ToByte(query.Count());
                }
            }

            else if (!String.IsNullOrEmpty(queryFilterResource.Search))
            {
                query = query.Where(s =>
                    s.Name.ToLowerInvariant().Contains(queryFilterResource.Search.ToLowerInvariant()));
            }

            var columnMap = new Dictionary<string, Expression<Func<Supplier, object>>>
            {
                ["supplierRef"] = s => s.SupplierRef,
                ["id"] = s => s.Id,
                ["name"] = s => s.Name,
            };

            result.TotalItems = await query.CountAsync();

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();


            return _mapper.Map<QueryResult<Supplier>, QueryResult<SupplierResource>>(result);
        }

        public async Task<SupplierResource> Add(ResourceEntity resourceEntity)
        {
            var supplierSaveResource = (SupplierSaveResource)resourceEntity;
            var supplier = _mapper.Map<SupplierSaveResource, Supplier>(supplierSaveResource);

            if (SupplierExist(supplier.SupplierRef, supplier.Name))
            {
                throw new Exception("Supplier name or ref already exists");
            }

            Repository.Add(supplier);

            await SaveChanges();
            await SaveHistoryAction("Add", supplierSaveResource);

            supplier = await Repository.GetOne<Supplier>().Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == supplier.Id);

            return _mapper.Map<Supplier, SupplierResource>(supplier);
        }

        public async Task<SupplierResource> Update(int id, ResourceEntity resourceEntity)
        {
            var Supplier = await Repository.GetOne<Supplier>().Include(p => p.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Supplier == null)
            {
                throw new Exception("catalog not found");
            }

            var SupplierSave = (SupplierSaveResource)resourceEntity;

            _mapper.Map(SupplierSave, Supplier);

            Supplier.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", SupplierSave);

            var result = await GetItemById(Supplier.Id);
            return result;
        }

        private bool SupplierExist(string supplierRef, string name)
        {
            var suppliers = Repository.GetAll<Supplier>();

            return suppliers != null && suppliers.Any(x => x.SupplierRef == supplierRef) || suppliers.Any(x => x.Name == name);

        }


    }
}

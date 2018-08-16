using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.Child;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class ChildManager : IChildManager
    {
        private readonly IMapper _mapper;
        public ISaveHistoryActionData SaveHistoryActionData { get; }
        private IRepository<JceDbContext> Repository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public ChildManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                UserId = userProfile.CreatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = userProfile.CreatedBy,
                UpdatedBy = userProfile.UpdatedBy,
                TableName = "child"
            };
            await SaveHistoryActionData.SaveHistory(resource);
        }


        

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }
        public async Task Delete(int id)
        {
            var child = await Repository.GetOne<Child>().FirstOrDefaultAsync(v => v.Id == id);

            if (child == null)
                throw new Exception("child not Found");

            var resource = _mapper.Map<Child, ChildResource>(child);

            Repository.Remove(child);
            await  SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

        public async Task<ChildResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var child = await Repository.GetOne<Child>().FirstOrDefaultAsync(v => v.Id == id);

            if (child == null)
                throw new Exception("child not Found");

            return _mapper.Map<Child, ChildResource>(child);
        }

        public async Task<ChildResource> Add(ResourceEntity resourceEntity)
        {
            var saveChild = (ChildSaveResource)resourceEntity;

            var parent = await Repository.GetOne<JceProfile>().FirstOrDefaultAsync(p => p.Id == saveChild.PersonJceProfileId);
            if (parent != null)
            {
                var child = _mapper.Map<ChildSaveResource, Child>(saveChild);

                Repository.Add(child);

                await SaveChanges();
                await SaveHistoryAction("ADD", saveChild);

                return _mapper.Map<Child, ChildResource>(child);
            }
            throw new Exception("child parent not Found");

        }

        public async Task<ChildResource> Update(int id, ResourceEntity resourceEntity)
        {
            var childSave = (ChildSaveResource)resourceEntity;

            var child = await Repository.GetOne<Child>().FirstOrDefaultAsync(v => v.Id == id);

            if (child == null)
                throw new Exception("child not Found");
           
            _mapper.Map(childSave, child);
            child.UpdatedOn = DateTime.Now;
            await SaveChanges();
            await SaveHistoryAction("Update", childSave);

            var result = await GetItemById(child.Id);
            return result;
        }

        public async Task<QueryResult<ChildResource>> GetAll(FilterResource filterResource)
        {
            var result = new QueryResult<Child>();

            var query = Repository.GetAll<Child>().AsQueryable();
            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Child>, QueryResult<ChildResource>>(result);
        }

       
    }
}

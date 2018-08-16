using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
//        public ISaveHistoryActionData SaveHistoryActionData { get; }
        private IRepository<IdentityServerDbContext> Repository { get; }

        public IUnitOfWork UnitOfWork { get; }

        public RoleManager(IRepository<IdentityServerDbContext> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
//            SaveHistoryActionData = saveHistoryActionData;
            Repository = repository;
            UnitOfWork = unitOfWork;
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
                TableName = "userProfile"
            };

//            await SaveHistoryActionData.SaveHistory(resource);
        }

      

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoIdentityDbContextAsync();
        }

        public async Task Delete(int id)
        {
            var role = await Repository.GetOne<Role>().FirstOrDefaultAsync(v => v.Id == id);

            if (role == null)
                throw new Exception("RoleId not Found");
            var resource = _mapper.Map<Role, RoleResource>(role);
             
            Repository.Remove(role);
            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }

     
        public async Task<RoleResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var role = await Repository.GetOne<Role>().FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<Role, RoleResource>(role);
        }

        public async Task<RoleResource> Add(ResourceEntity resourceEntity)
        {
            var saveRole = (RoleResource) resourceEntity;

            if (RoleExist(saveRole.Name))
            {
                throw new Exception("RoleId exist");
            }
            var role = _mapper.Map<RoleResource, Role>(saveRole);

            Repository.Add(role);

            await SaveChanges();
            await SaveHistoryAction("Add", saveRole);


            return _mapper.Map<Role, RoleResource>(role);
        }

        public async Task<QueryResult<RoleResource>> GetAll(FilterResource filterResource)
        {
            var result = new QueryResult<Role>();

            var query =  Repository.GetAll<Role>().AsQueryable();
            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Role>, QueryResult<RoleResource>>(result);
        }

        public bool RoleExist(string role)
        {
            var roles = Repository.GetAll<Role>();

            return roles != null && roles.Any(x =>  string.Equals(x.Name, role, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<RoleResource> Update(int id, ResourceEntity resourceEntity)
        {
            var roleSave = (RoleResource) resourceEntity;
            var role = await Repository.GetOne<Role>().FirstOrDefaultAsync(v => v.Id == id);

            if (role == null)
                throw new Exception("employee not Found");
            if (roleSave.Name != role.Name && RoleExist(roleSave.Name))
            {
                throw new Exception("role name " + roleSave.Name + " is already taken");

            }
            _mapper.Map(resourceEntity, role);
            role.UpdatedOn = DateTime.Now;
            await SaveChanges();
            await SaveHistoryAction("Update", roleSave);

            var result = await GetItemById(role.Id);
            return result;
        }
    }
}

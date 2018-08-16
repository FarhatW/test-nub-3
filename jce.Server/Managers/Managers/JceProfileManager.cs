using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.Helpers;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Entites.JceDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.adminProfile;
using jce.Common.Resources.personProfile;
using jce.Common.Resources.UserProfile;
using jce.Common.Setting;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Managers
{
    public class JceProfileManager : IJceProfileManager
    {
        private readonly AuthSetting _optionsUserProfile;

        public IUnitOfWork UnitOfWork { get; }

        private readonly IMapper _mapper;
        private ISaveHistoryActionData SaveHistoryActionData { get; }
        private IRepository<JceDbContext> Repository { get; }

        public JceProfileManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData,
            IUnitOfWork unitOfWork, IMapper mapper, IOptions<AuthSetting> options)
        {
            SaveHistoryActionData = saveHistoryActionData;
            _optionsUserProfile = options.Value;
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
                TableName = "jceProfile"
            };
            await SaveHistoryActionData.SaveHistory(resource);
        }


        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public async Task Delete(int id)
        {
            var saveEmployee = await Repository.GetOne<PersonJceProfile>().FirstOrDefaultAsync(v => v.Id == id);

            if (saveEmployee == null)
                throw new Exception("employee not Found");

            var employee = _mapper.Map<PersonJceProfile, JceProfileResource>(saveEmployee);
            Repository.Remove(saveEmployee);
            await SaveChanges();
            await SaveHistoryAction("Delete", employee);
        }



        public async Task<JceProfileResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var user = await Repository.GetOne<JceProfile>().FirstOrDefaultAsync(v => v.Id == id);
            if (user == null) throw new Exception("user not Found");

            if (user.GetType() == typeof(AdminJceProfile))
            {
                user = await Repository.GetOne<AdminJceProfile>().Include(a => a.Ces)
                    .FirstOrDefaultAsync(u => u.Id == id);
                return _mapper.Map<JceProfile, AdminProfileResource>(user);
            }
        
                user = await Repository.GetOne<PersonJceProfile>().Include(c => c.Children).FirstOrDefaultAsync(u => u.Id == id);
                return _mapper.Map<JceProfile, PersonProfileResource>(user);
            
        }

        public  Task<JceProfileResource> Add(ResourceEntity resourceEntity)
        {
            return null;
        }

        public Task<JceProfileResource> Update(int id, ResourceEntity resourceEntity)
        {
            return null;
        }

        public async Task<QueryResult<JceProfileResource>> GetAll(FilterResource filterResource)
        {
            var queryResource = (JceProfileQueryResource)filterResource;
            var result = new QueryResult<JceProfile>();
            var queryObj = _mapper.Map<JceProfileQueryResource, JceProfileQuery>(queryResource);

            var columMap = new Dictionary<string, Expression<Func<JceProfile, object>>>
            {
                ["id"] = v => v.Id,
                ["email"] = v => v.Email,
                ["firstName"] = v => v.FirstName,

            };

            var admins = Repository.GetAll<AdminJceProfile>().Include(ad => ad.Ces).ToList();
            var person = Repository.GetAll<PersonJceProfile>().Include( p => p.Children).ToList();


            var query = admins.Concat<JceProfile>(person).AsQueryable();

            if (queryResource.UserType.HasValue)
            {
                switch (queryResource.UserType)
                {
                    case 1:
                        query = admins.AsQueryable();
                        break;
                    case 2:
                        query = person.AsQueryable();
                        break;
                    default:
                        throw new Exception("user type not exist");
                }
              
            }
            else if (!string.IsNullOrEmpty(queryResource.Search))
            {
                query = query.Where(p => p.Id.ToString().Contains(queryResource.Search.ToLowerInvariant())
                                         || (!string.IsNullOrEmpty(p.FirstName) && 
                                         p.FirstName.ToLowerInvariant().Contains(queryResource.Search.ToLowerInvariant())));

               
            }
          
            

            queryObj.PageSize = ManagersHelper.CheckPageSize(queryResource, query);

            result.TotalItems = query.Count();

            query = query.ApplyOrdering(queryObj, columMap);

            query = query.ApplyPaging(queryObj);

          

            result.Items = query.ToList();
            var a = _mapper.Map<QueryResult<JceProfile>, QueryResult<JceProfileResource>>(result);
            return a;
        }

        public async Task<List<ChildResource>> GetChildByUserProfileId(int UserProfileId)
        {
            var query = await Repository.GetAll<Child>().AsQueryable().Where(c => c.PersonJceProfileId == UserProfileId)
                .ToListAsync();

            var result = _mapper.Map<List<Child>, List<ChildResource>>(query);

            return result;

        }


      
        public async Task<AdminProfileResource> AddAdminJceProfile(ResourceEntity resourceEntity)
        {
            var saveProfileResource = (AdminProfileSaveResource)resourceEntity;


            if (EmailExist(saveProfileResource.Email))
                throw new Exception("User name " + saveProfileResource.Email + " is already taken");

            var userProfile = _mapper.Map<AdminProfileSaveResource, AdminJceProfile>(saveProfileResource);


            Repository.Add(userProfile);
            await SaveChanges();
            await SaveHistoryAction("AddJceP", saveProfileResource);


            return _mapper.Map<AdminJceProfile, AdminProfileResource>(userProfile);
        }

        public async Task<AdminProfileResource> UpdateAdminJceProfile(int id, ResourceEntity resourceEntity)
        {
            var saveUserProfileResource = (AdminProfileSaveResource)resourceEntity;

            var userProfile = await Repository.GetOne<AdminJceProfile>().FirstOrDefaultAsync(v => v.Id == id);

            if (userProfile == null)
                throw new Exception("employee not Found");

            if (saveUserProfileResource.Email != userProfile.Email && EmailExist(saveUserProfileResource.Email))
            {
                throw new Exception("User name " + saveUserProfileResource.Email + " is already taken");

            }

            _mapper.Map(resourceEntity, userProfile);

            userProfile.UpdatedOn = DateTime.Now;

            await SaveChanges();

            await SaveHistoryAction("Update", saveUserProfileResource);

            var result = await Repository.GetOne<AdminJceProfile>().FirstOrDefaultAsync(v => v.Id == userProfile.Id);

            return _mapper.Map<AdminJceProfile, AdminProfileResource>(result);
        }

        public async Task<PersonProfileResource> AddPersonJceProfile(ResourceEntity resourceEntity)
        {
            var saveProfileResource = (PersonProfileSaveResource)resourceEntity;


            if (EmailExist(saveProfileResource.Email))
                throw new Exception("User name " + saveProfileResource.Email + " is already taken");

            var userProfile = _mapper.Map<PersonProfileSaveResource, PersonJceProfile>(saveProfileResource);


            Repository.Add(userProfile);
            await SaveChanges();
            await SaveHistoryAction("AddJceP", saveProfileResource);


            return _mapper.Map<PersonJceProfile, PersonProfileResource>(userProfile);
        }

        public async Task<PersonProfileResource> UpdatePersonJceProfile(int id, ResourceEntity resourceEntity)
        {
            var saveUserProfileResource = (PersonProfileSaveResource)resourceEntity;

            var userProfile = await Repository.GetOne<PersonJceProfile>().FirstOrDefaultAsync(v => v.Id == id);

            if (userProfile == null)
                throw new Exception("employee not Found");

            if (saveUserProfileResource.Email != userProfile.Email && EmailExist(saveUserProfileResource.Email))
            {
                throw new Exception("User name " + saveUserProfileResource.Email + " is already taken");

            }

            _mapper.Map(resourceEntity, userProfile);

            userProfile.UpdatedOn = DateTime.Now;

            await SaveChanges();

            await SaveHistoryAction("Update", saveUserProfileResource);

            var result = await Repository.GetOne<PersonJceProfile>().FirstOrDefaultAsync(v => v.Id == userProfile.Id);

            return _mapper.Map<PersonJceProfile, PersonProfileResource>(result);
        }

        private bool EmailExist(string email)
        {
            var employees = Repository.GetAll<PersonJceProfile>();

            return employees != null && employees.Any(x => x.Email == email);

        }

    }
}

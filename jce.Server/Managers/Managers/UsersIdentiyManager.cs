using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using jce.BusinessLayer.IManagers;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources;
using jce.Common.Resources.user;
using jce.Common.Resources.userIdentity;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Managers
{
   public class UsersIdentiyManager : IUserIdentiyManager
    {
        public IUnitOfWork UnitOfWork { get; }
        private readonly IMapper _mapper;
        private readonly IRoleManager _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityServerInteractionService _interaction;
        private IRepository<IdentityServerDbContext> Repository { get; }
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        public UsersIdentiyManager(IUnitOfWork unitOfWork, IMapper mapper, IRepository<IdentityServerDbContext> repository, IRoleManager roleManager, UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory, IHttpContextAccessor httpContextAccessor, IIdentityServerInteractionService interaction)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;
            _roleManager = roleManager;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _httpContextAccessor = httpContextAccessor;
            _interaction = interaction;
        }

     
        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoIdentityDbContextAsync();
        }

        public async Task Delete(int id)
        {
            var user = await GetItemById(id);

            if (user == null)
                throw new Exception("user not Found");

            Repository.Remove(user);
            await SaveChanges();
        }

        public Task SaveHistoryAction(string action, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var user = await Repository.GetOne<User>().Include(v => v.UserRoles)
                .ThenInclude(vf => vf.Role).FirstOrDefaultAsync(v => v.Id == id);
            var result = _mapper.Map<User, UserResource>(user);
            return result;
        }

        public async Task<UserResource> Add(ResourceEntity resourceEntity)
        {
            var saveUserResource = (SaveUserResource)resourceEntity;
            if (!saveUserResource.Roles.Any())  throw new Exception("role is required");

            if (!await RoleExiste(saveUserResource.Roles))
            {
                throw new Exception("role dont exist, valid role is required");
            }
            var user = new User { UserName = RandomString(5) + "@.aze.com", Email = RandomString(5) + "@.aze.com" };
            
            var result = await _userManager.CreateAsync(user, saveUserResource.Password);
            if (!result.Succeeded)  throw new Exception(result.Errors.ToString());
            var mappedUser = _mapper.Map<User, SaveUserResource>(user);
            mappedUser.Roles = saveUserResource.Roles;


            var updatedUser = await Update(user.Id, mappedUser);
            var claims = await GetUserClaims(user);
            await _userManager.AddClaimsAsync(user, claims);

            return updatedUser;
           
        }

        public async Task<UserResource> Update(int id, ResourceEntity resourceEntity)
        {
            var saveUserResource = (UpdateUserResource)resourceEntity;
            var user = await Repository.GetOne<User>().Include(u => u.UserRoles)
                .ThenInclude(vf => vf.Role).FirstOrDefaultAsync(v => v.Id == id);


            if (user == null)
                throw new Exception("user not Found");

            if (saveUserResource.Email != user.Email && EmailExist(saveUserResource.Email))
            {
                throw new Exception("email exist");
            }

            _mapper.Map(saveUserResource, user);
            //await Helper.UpdateUserDatas(saveUserResource, user, this);

            await SaveChanges();


            return _mapper.Map<User, UserResource>(user);
        }

        public async Task<QueryResult<UserResource>> GetAll(FilterResource filterResource)
        {
            var queryResource = (UserQueryResource)filterResource;
            var result = new QueryResult<User>();
            var filters = _mapper.Map<UserQueryResource, UserQuery>(queryResource);

            var query = Repository.GetAll<User>().Include(v => v.UserRoles)
                .ThenInclude(vf => vf.Role).AsQueryable();

            var columMap = new Dictionary<string, Expression<Func<User, object>>>
            {
                ["id"] = v => v.Id,
                ["email"] = v => v.Email
 
            };

            var queryObj = _mapper.Map<UserQueryResource, UserQuery>(queryResource);

                result.TotalItems = await query.CountAsync();
            
                query = query.ApplyOrdering(queryObj, columMap);
            
                query = query.ApplyPaging(queryObj);
            
                result.Items = await query.ToListAsync();

            return _mapper.Map<QueryResult<User>, QueryResult<UserResource>>(result);
        }
        public bool EmailExist(string email)
        {
            var employees = Repository.GetAll<User>();

            return employees != null && employees.Any(x => x.Email == email);

        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> RoleExiste(ICollection<int> roles)
        {

            var result = false;
            foreach (var role in roles)
            {
                result = await _roleManager.GetItemById(role) != null;
            }

            return result;
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(User user)
        {
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            claims.AddRange(user.UserRoles.Select(item => new Claim(ClaimTypes.Role, item.Role.Name)));

            return claims;
        }

        public async Task<LogOutViewResource> LogoutAsync(string logoutId)
        {
            var vm = new LogOutViewResource{ LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            var user = _httpContextAccessor.HttpContext.User;
            if (user?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt != false) return vm;
            // it's safe to automatically sign-out
            vm.ShowLogoutPrompt = false;
            return vm;

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
        }

        public async Task<LoggedOutResource> LoggedOutAsync(string logoutId)
        {
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutResource()
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = logout?.ClientId,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            var user = _httpContextAccessor.HttpContext.User;
            if (user?.Identity.IsAuthenticated != true) return vm;
            var idp = user.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
            if (idp == null || idp == IdentityServer4.IdentityServerConstants.LocalIdentityProvider) return vm;
            var providerSupportsSignout = await _httpContextAccessor.HttpContext.GetSchemeSupportsSignOutAsync(idp);
            if (!providerSupportsSignout) return vm;
            if (vm.LogoutId == null)
            {
                // if there's no current logout context, we need to create one
                // this captures necessary info from the current logged in user
                // before we signout and redirect away to the external IdP for signout
                vm.LogoutId = await _interaction.CreateLogoutContextAsync();
            }

            vm.ExternalAuthenticationScheme = idp;

            return vm;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.user;
using jce.Common.Resources.userIdentity;

namespace jce.BusinessLayer.IManagers
{
    public interface IUserIdentiyManager : IActionManager<UserResource>
    {
        Task<LogOutViewResource> LogoutAsync(string logoutId);

        Task<LoggedOutResource> LoggedOutAsync(string logoutId);
    } 
}

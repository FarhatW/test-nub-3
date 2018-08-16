using System.Collections.Generic;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Core;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.adminProfile;
using jce.Common.Resources.personProfile;
using jce.Common.Resources.UserProfile;

namespace jce.BusinessLayer.IManagers
{
    public interface IJceProfileManager : IActionManager<JceProfileResource>
    {

        Task<AdminProfileResource> AddAdminJceProfile(ResourceEntity resourceEntity);
        Task<AdminProfileResource> UpdateAdminJceProfile(int id, ResourceEntity resourceEntity);
        Task<PersonProfileResource> AddPersonJceProfile(ResourceEntity resourceEntity);
        Task<PersonProfileResource> UpdatePersonJceProfile(int id, ResourceEntity resourceEntity);


    }
}

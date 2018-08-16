using System.Collections.Generic;
using System.Collections.ObjectModel;
using jce.Common.Core;
using jce.Common.Entites.IdentityServerDbContext;

namespace jce.Common.Resources.user
{
    public class UserResource : ResourceEntity

    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }


        public ICollection<Role> Roles { get; set; }

        public UserResource()
        {
            Roles = new Collection<Role>();
        }

    }
}
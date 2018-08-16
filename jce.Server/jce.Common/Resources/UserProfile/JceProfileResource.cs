using System.Collections.Generic;
using System.Collections.ObjectModel;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.IdentityServerDbContext;

namespace jce.Common.Resources.UserProfile
{
    public  class JceProfileResource : ResourceEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool isEnabled { get; set; }
        public AddressResource Address { get; set; }

    }
}
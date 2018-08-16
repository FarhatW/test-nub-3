using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.CE;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Resources.adminProfile
{
    public class AdminProfileResource : JceProfileResource
    {
        public ICollection<Ce> CEs { get; set; }

        public AdminProfileResource()
        {
            CEs = new List<Ce>();
        }

      
    }
}

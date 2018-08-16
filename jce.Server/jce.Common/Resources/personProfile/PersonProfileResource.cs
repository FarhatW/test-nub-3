using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Resources.personProfile
{
   public class PersonProfileResource : JceProfileResource
    {
        public int CeId { get; set; }
        public ICollection<ChildResource> Children { get; set; }
        
        public PersonProfileResource()
        {
            Children = new List<ChildResource>();
        }
    }
}

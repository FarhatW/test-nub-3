using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Resources.CE;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Resources.adminProfile
{
   public class AdminProfileSaveResource : JceProfileSaveResource
    {
        public ICollection<int> CEs { get; set; }
        public string CommercialUniqueCode { get; set; }
        public AdminProfileSaveResource()
        {
            CEs = new List<int>();
        }
    }
}

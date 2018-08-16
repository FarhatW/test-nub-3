using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources.CE
{
    public class CeQueryResource : FilterResource
    {
        public bool? CeSetupQuery { get; set; }
        public bool? CatalogQuery { get; set; }
        public bool? CeInfosQuery { get; set; }
        public bool? CeUserProfilesQuery { get; set; }
        public int? IdAdmin { get; set; }
        public string Search { get; set; }




    }
}

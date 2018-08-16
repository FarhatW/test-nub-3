using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class CeQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public bool? CeSetupQuery { get; set; }
        public bool? CatalogQuery { get; set; }
        public bool? CeInfosQuery { get; set; }
        public bool? CeUserProfilesQuery { get; set; }

        public int? IdAdmin { get; set; }
        public string Search { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;
using jce.Common.Resources;

namespace jce.Common.Query
{
    public class FileQuery : IQueryObject
    {
        public bool? IsImage { get; set; }


        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

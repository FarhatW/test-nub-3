using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class PintelSheetQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string PintelSheetArray { get; set; }
        public bool? ReturnProducts { get; set; }

    }
}

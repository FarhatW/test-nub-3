using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class GoodQuery : IQueryObject
    {
        public int? PintelSheet { get; set; }
        public string RefPintel { get; set; }
        public string RefPintelArray { get; set; }
        public string ProductIndex { get; set; }
        public string Search { get; set; }
        public string PintelSheetArray { get; set; }
        public bool? IsBatch { get; set; }



        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

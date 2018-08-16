using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class ProductQuery : IQueryObject
    {

        public int? PintelSheet { get; set; }
        public string RefPintel { get; set; }
        public string RefPintelArray { get; set; }
        public string ProductLettersArray { get; set; }


        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

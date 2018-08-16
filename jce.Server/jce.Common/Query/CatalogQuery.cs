using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Core.EnumClasses;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class CatalogQuery : IQueryObject
    {

        public string DateMin { get; set; }
        public string DateMax { get; set; }
        public string BirthDate { get; set; }
        public bool? CeId { get; set; }
        public string PintelSheetsArray { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

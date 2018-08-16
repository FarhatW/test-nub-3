using jce.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace jce.Common.Query
{
    public class ScheduleQuery : IQueryObject
    {
        public string SortBy { get ; set ; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? EventId { get; set; }
    }
}

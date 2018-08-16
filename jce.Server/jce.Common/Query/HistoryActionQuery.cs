using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class HistoryActionQuery : IQueryObject
    {
        public int? UserId { get; set; }
        public string ActionName { get; set; }
        public string TableName { get; set; }

        public string Date { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

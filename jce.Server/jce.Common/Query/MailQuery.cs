using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class MailQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

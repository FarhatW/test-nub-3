using jce.Common.Extentions;

namespace jce.Common.Query
{
    public class JceProfileQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool? NoPageSize { get; set; }
    }
}

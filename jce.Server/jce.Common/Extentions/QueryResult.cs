using System.Collections.Generic;
using System.Linq;
using jce.Common.Entites;

namespace jce.Common.Extentions
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }

    }
}
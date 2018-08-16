using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using jce.Common.Entites;

namespace jce.Common.Extentions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columsMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !columsMap.ContainsKey(queryObj.SortBy))
                return query;

            return queryObj.IsSortAscending ? query.OrderBy(columsMap[queryObj.SortBy]) : query.OrderByDescending(columsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 1;

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}
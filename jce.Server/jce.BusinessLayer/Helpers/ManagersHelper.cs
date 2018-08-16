using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.Common.Core;
using jce.Common.Extentions;
using jce.Common.Resources;

namespace jce.BusinessLayer.Helpers
{
    public class ManagersHelper
    {
        public static int CheckPageSize(FilterResource queryResource, IQueryable<BaseEntity> entity)
        {
            var pageSize = 0;

            switch (queryResource.PageSize)
            {
                case 0:
                {
                    pageSize = entity.Count();
                    break;
                }

                default:
                {
                    pageSize = queryResource.PageSize;
                    break;
                }
            }

            return pageSize;
        }
    }
}

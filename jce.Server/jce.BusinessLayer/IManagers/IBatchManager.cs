using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Resources.Batch;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace jce.BusinessLayer.IManagers
{
    public interface IBatchManager : IActionManager<BatchResource>
    {
        Task<BatchListResource> MultipleAdd(BatchSaveResource[] batchSaveResourceArray);
    }
}

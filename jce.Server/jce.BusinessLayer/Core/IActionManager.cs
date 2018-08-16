using System.Collections.Generic;
using System.Threading.Tasks;
using jce.Common.Core;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.DataAccess.Core;

namespace jce.BusinessLayer.Core
{
    public interface IActionManager<T>
    {
        IUnitOfWork UnitOfWork { get; }
        Task SaveChanges();
        Task Delete(int id);
        Task SaveHistoryAction(string action, ResourceEntity resourceEntity);

        Task<T> GetItemById(int id, FilterResource filterResource = null);
        Task<T> Add(ResourceEntity resourceEntity);
        Task<T> Update(int id, ResourceEntity resourceEntity);

        Task<QueryResult<T>> GetAll(FilterResource filterResource);
    }
}

using System.Collections.Generic;
using System.Linq;

namespace jce.DataAccess.Core
{
    /// <summary>
    /// 
    /// </summary>
       public interface IRepository<u>

    {
        u Context { get; }
        IQueryable<T> GetOne<T>() where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        void Add<T>(T tObject) where T : class;
        void Remove<T>(T tObject) where T : class;
    }
}

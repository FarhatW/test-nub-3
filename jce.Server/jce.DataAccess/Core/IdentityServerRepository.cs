using System.Collections.Generic;
using System.Linq;
using jce.DataAccess.Core.dbContext;

namespace jce.DataAccess.Core
{
   public class IdentityServerRepository : IRepository<IdentityServerDbContext>
    {
        public IdentityServerDbContext Context { get; }

        public IdentityServerRepository(IDbFactory dbFactory)
        {
            Context = dbFactory.GetIdentityServerDbContext;
        }

        public IQueryable<T> GetOne<T>() where T : class
        {
            return Context.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Context.Set<T>().AsQueryable();
        }

        public void Add<T>(T tObject) where T : class
        {
            Context.Set<T>().Add(tObject);
        }
        public void AddMultiple<T>(List<T> tObject) where T : class
        {
            Context.Set<List<T>>().Add(tObject);
        }

        public void Remove<T>(T tObject) where T : class
        {
            Context.Set<T>().Remove(tObject);
        }
    }
}

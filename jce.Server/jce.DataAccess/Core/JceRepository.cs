using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.DataAccess.Core.dbContext;
using Microsoft.EntityFrameworkCore;

namespace jce.DataAccess.Core
{
   public class JceRepository : IRepository<JceDbContext>
    {
        public JceDbContext Context { get; }
        public JceRepository(IDbFactory dbFactory)
        {
            Context = dbFactory.GetJceDbContext;
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

        public void Remove<T>(T tObject) where T : class
        {
            Context.Set<T>().Remove(tObject);
        }

    }

}


using jce.DataAccess.Core.dbContext;

namespace jce.DataAccess.Core
{
    public class DbFactory : IDbFactory
    {



       

        public JceDbContext GetJceDbContext { get; }

        public IdentityServerDbContext GetIdentityServerDbContext { get; }

        public DbFactory(JceDbContext getJceDbContext)
        {
            GetJceDbContext = getJceDbContext;

        }

        public DbFactory( IdentityServerDbContext getIdentityServerDbContext)
        {
            GetIdentityServerDbContext = getIdentityServerDbContext;
        }
    }
}

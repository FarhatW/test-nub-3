using jce.DataAccess.Core.dbContext;

namespace jce.DataAccess.Core
{
   public interface IDbFactory
    {
        JceDbContext GetJceDbContext { get; }
        IdentityServerDbContext GetIdentityServerDbContext { get; }
    }
}

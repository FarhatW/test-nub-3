using System.Threading.Tasks;

namespace jce.DataAccess.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }




        public async Task SaveIntoJceDbContextAsync()
        {
            await _dbFactory.GetJceDbContext.SaveChangesAsync();
        }

        public async Task SaveIntoIdentityDbContextAsync()
        {
            await _dbFactory.GetIdentityServerDbContext.SaveChangesAsync();
        }
    }
}
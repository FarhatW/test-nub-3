using System.Threading.Tasks;

namespace jce.DataAccess.Core
{
    public interface IUnitOfWork
    {



        Task SaveIntoJceDbContextAsync();
        Task SaveIntoIdentityDbContextAsync();
    }
}
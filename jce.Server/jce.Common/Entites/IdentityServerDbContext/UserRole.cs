
using Microsoft.AspNetCore.Identity;

namespace jce.Common.Entites.IdentityServerDbContext
{
    
    public class UserRole :  IdentityUserRole<int>
    {
        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
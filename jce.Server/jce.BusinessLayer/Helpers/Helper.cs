using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.UserProfile;
using jce.DataAccess.Core;

namespace jce.BusinessLayer.Helpers
{
    public class Helper
    {

        public static PersonJceProfile UpdateUserProfileDatas(JceProfileSaveResource userProfileResource, PersonJceProfile personJceProfile, IJceProfileManager jceProfileManager)
        {
            // update password if it was entered
           

            return personJceProfile;
        }
      

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", nameof(password));
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).",
                $"passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).",
                $"passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}

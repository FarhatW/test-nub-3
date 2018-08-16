using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources.userIdentity
{
   public class UpdateUserResource : ResourceEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public bool LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

      

        [Required]
        public ICollection<int> Roles { get; set; }


        public UpdateUserResource()
        {
            Roles = new Collection<int>();
        }
      



    }
}

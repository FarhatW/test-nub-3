using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using jce.Common.Core;
using jce.Common.Entites;

namespace jce.Common.Resources.UserProfile
{
    public abstract class JceProfileSaveResource : ResourceEntity
    {
            public int Id { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string Email { get; set; }

            public string Phone { get; set; }

            public bool IsEnabled { get; set; }
            [Required]
            public AddressResource Address { get; set; }

          
       
     

       
    }
}
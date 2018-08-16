using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Entites;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Resources.personProfile
{
   public class PersonProfileSaveResource : JceProfileSaveResource
    {
        [Required]
        public int CeId { get; set; }
        
    
        public PersonProfileSaveResource()
        {

        }
    }
}

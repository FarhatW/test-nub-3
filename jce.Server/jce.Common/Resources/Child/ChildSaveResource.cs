using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources.Child
{
   public class ChildSaveResource : ResourceEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public bool IsActif { get; set; }

        [Required]
        public decimal? AmountParticipationCe { get; set; }
        
        [Required]
        public int? PersonJceProfileId { get; set; }
    }
}

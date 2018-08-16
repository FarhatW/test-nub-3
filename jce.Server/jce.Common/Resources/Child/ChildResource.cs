using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources
{
    public class ChildResource :ResourceEntity
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        public bool? IsActif { get; set; }

        public decimal AmountParticipationCe { get; set; }

        public bool? IsRegrouper { get; set; }

        [Required]
        public int PersonJceProfileId { get; set; }
    }
}

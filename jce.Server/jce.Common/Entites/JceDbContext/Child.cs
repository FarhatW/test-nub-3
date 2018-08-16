using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using jce.Common.Core;

namespace jce.Common.Entites.JceDbContext
{
    [Table("Children")]
    public class Child :BaseEntity
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
        [ForeignKey("PersonJceProfiles")]
        public int PersonJceProfileId { get; set; }

    }
}
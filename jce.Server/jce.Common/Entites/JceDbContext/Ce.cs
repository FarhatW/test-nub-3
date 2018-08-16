using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using jce.Common.Core;

namespace jce.Common.Entites.JceDbContext
{
    [Table("Ces")]
    public class Ce : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }

        [ForeignKey("AdminJceProfiles")]
        public int AdminJceProfileId { get; set; }
        public bool IsDeleted { get; set; }
        public bool? Actif { get; set; }

        //address & contact
        [Required]
        public string Telephone { get; set; }
        public string Fax { get; set; }

        [Required]
        public string Company { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        public string AddressExtra { get; set; }

        public Catalog Catalog { get; set; }
        public CeSetup CeSetup { get; set; }
        public ICollection<PersonJceProfile> UserProfiles { get; set; }
        public ICollection<Event> Events { get; set; }


        public Ce()
        {
            UserProfiles = new Collection<PersonJceProfile>();
            Events = new Collection<Event>();
        }

    }
}
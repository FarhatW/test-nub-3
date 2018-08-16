using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Entites
{
    [Table("Catalogs")]
    public class Catalog : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("CEs")]
        public int CeId { get; set; }
        public string IndexId { get; set; }
        [Required]
        public int CatalogType { get; set; }
        public bool? IsActif { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool DisplayPrice { get; set; }

        public int CatalogChoiceTypeId { get; set; }
        public int? ProductChoiceQuantity { get; set; }

        public int? BooksQuantity { get; set; }
        public int? ToysQuantity { get; set; }
        public int? SubscriptionQuantity { get; set; }

        public ICollection<CatalogGood> CatalogGoods { get; set; }

        public Catalog()
        {
            CatalogGoods = new Collection<CatalogGood>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Entites.JceDbContext
{
    [Table("Suppliers")]
    public class Supplier : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsEnabled { get; set; }
        [Required]
        [StringLength(20)]
        public string SupplierRef { get; set; }

        public ICollection<Product> Products { get; set; }

        public Supplier()
        {
            Products = new Collection<Product>();
        }


    }
}

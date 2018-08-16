using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using jce.Common.Core;
using jce.Common.Resources.Product;

namespace jce.Common.Resources.Supplier
{
    public class SupplierResource : ResourceEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsEnabled { get; set; }
        public string SupplierRef { get; set; }
        public int ProductCount { get; set; }

        public ICollection<ProductResource> Products { get; set; }

        public SupplierResource()
        {
            Products = new Collection<ProductResource> ();
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace jce.Common.Entites.JceDbContext
{
    [Table("Batches")]
    public class Batch : Good
    {
        public ICollection<Product> Products { get; set; }

        public Batch(string details, int id, int price, string indexId, string refPintel, string title, bool isEnabled, ICollection<Product> products, bool isDiscountable, int goodDepartment, int productType)
            : base(id, details, price, indexId, refPintel, title, isEnabled, isDiscountable, goodDepartment, productType)
        {
            this.Products = products;
        }

        public Batch()
        {
            Products = new Collection<Product>();
        }

        public override void Add(Product component)
        {
            Products.Add(component);
        }

        public override void Remove(Product component)
        {

        }

   
    }
}

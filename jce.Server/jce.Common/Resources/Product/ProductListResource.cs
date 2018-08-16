using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace jce.Common.Resources.Product
{
    public class ProductListResource
    {
        public ICollection<ProductResource> Products { get; set; }
        public int AddedProductCount { get; set; }
        public int NotAddedProductCount { get; set; }
        public List<string> DuplicatedRefList { get; set; }

        public ProductListResource()
        {
            Products = new Collection<ProductResource>();
            DuplicatedRefList = new List<string>();
        }
    }
}

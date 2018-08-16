using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using Newtonsoft.Json;

namespace jce.Common.Resources.Batch
{
    public class BatchSaveResource : GoodSaveResource
    {
        public ICollection<ProductSaveResource> Products;

        [JsonIgnore]
        public List<int> DeletedProds { get; set; }


        public BatchSaveResource()
        {
            Products = new Collection<ProductSaveResource>();
            DeletedProds = new List<int>();

        }
    }
}

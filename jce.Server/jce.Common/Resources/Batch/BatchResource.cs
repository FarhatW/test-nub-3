using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace jce.Common.Resources.Batch
{
    public class BatchResource : GoodResource 
    {
        [JsonIgnore]
        public List<int> DeletedProds { get; set; }

        public ICollection<ProductToBatchResource> Products { get; set; }

        public BatchResource()
        {
           Products = new Collection<ProductToBatchResource>();
           DeletedProds = new List<int>();
        }
      
    }
}

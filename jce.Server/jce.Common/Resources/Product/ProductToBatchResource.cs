using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Resources.Good;

namespace jce.Common.Resources.Product
{
    public class ProductToBatchResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string RefPintel { get; set; }
        public int SupplierId { get; set; }
        public int OriginId { get; set; }

        public string ProductType { get; set; }

    }
}

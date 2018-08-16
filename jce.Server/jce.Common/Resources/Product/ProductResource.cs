using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.Good;

namespace jce.Common.Resources.Product
{
    public class ProductResource : GoodResource
    {
        public bool? IsDisplayedOnJCE { get; set; }
        public bool? IsBasicProduct { get; set; }
        public int PintelSheetId { get; set; }
        public int SupplierId { get; set; }
        public int OriginId { get; set; }

        public ProductResource()
        {
            
        }

    }
}

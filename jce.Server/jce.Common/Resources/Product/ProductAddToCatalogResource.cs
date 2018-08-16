using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;

namespace jce.Common.Resources
{
    public class ProductAddToCatalogResource : GoodToCatalogResource
    {

        public bool? IsDisplayedOnJCE { get; set; }
        public bool? IsBasicProduct { get; set; }
        public int? PintelSheetId { get; set; }
        public string Brand { get; set; }

    }
}

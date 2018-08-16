using System;
using System.Collections.Generic;
using System.Text;

namespace jce.Common.Resources
{
    public class ProductQueryResource : FilterResource
    {
        public int? PintelSheet { get; set; }
        public string RefPintel { get; set; }
        public string RefPintelArray { get; set; }
        public string ProductLettersArray { get; set; }

    }
}

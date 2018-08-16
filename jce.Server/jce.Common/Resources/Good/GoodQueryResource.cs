using System;
using System.Collections.Generic;
using System.Text;

namespace jce.Common.Resources.Good
{
    public class GoodQueryResource : FilterResource
    {
        public int? PintelSheet { get; set; }
        public string RefPintel { get; set; }
        public string RefPintelArray { get; set; }
        public string ProductIndex { get; set; }
        public string PintelSheetArray { get; set; }


        public string Search { get; set; }
        public bool? IsBatch { get; set; }


    }
}

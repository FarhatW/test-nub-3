using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Core.EnumClasses;

namespace jce.Common.Resources
{
    public class CatalogQueryResource : FilterResource
    {
        public string DateMin { get; set; }
        public string DateMax { get; set; }
        public string BirthDate { get; set; }
        public bool? CeId { get; set; }
        public string PintelSheetsArray { get; set; }
        public string LettersArray { get; set; }


    }
}

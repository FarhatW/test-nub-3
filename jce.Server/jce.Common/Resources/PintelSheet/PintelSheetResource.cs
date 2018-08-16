using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;

namespace jce.Common.Resources.PintelSheet
{
    public class PintelSheetResource : ResourceEntity
    {
        public int Id { get; set; }
        public string SheetId { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string FilePath { get; set; }
        public string Season { get; set; }
        public int ProductCount { get; set; }

        public ICollection<Entites.Product> Products { get; set; }

        //public PintelSheetResource()
        //{
        //    Products = new Collection<Entites.Product>();

        //}

    }
}

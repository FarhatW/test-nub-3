using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;

namespace jce.Common.Resources.PintelSheet
{
    public class PintelSheetSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        public string SheetId { get; set; }
        public int AgeGroupId { get; set; }
        public string FilePath { get; set; }
        public string Season { get; set; }

        public ICollection<int> Products { get; set; }

        public PintelSheetSaveResource()
        {
            Products = new Collection<int>();
        }
    }
}

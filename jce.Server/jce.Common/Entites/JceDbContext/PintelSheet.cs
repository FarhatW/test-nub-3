using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;

namespace jce.Common.Entites
{
    /// <summary>
    /// Fiche Pintel
    /// </summary>
    [Table("PintelSheets")]
    public class PintelSheet : BaseEntity
    {
        public int Id { get; set; }
        public string SheetId { get; set; }
        public AgeGroup AgeGroup { get; set; }
        public string FilePath { get; set; }
        public string Season { get; set; }
        //public DateTime DateMin { get; set; }
        //public DateTime DateMax { get; set; }

        public ICollection<Product> Products { get; set; }

        public PintelSheet()
        {
            Products = new Collection<Product>();
        }

    }
}

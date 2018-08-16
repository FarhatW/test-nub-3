using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Entites.JceDbContext;

namespace jce.Common.Entites
{
    [Table("CatalogGoods")]
    public class CatalogGood
    {
        public int GoodId { get; set; }
        public int CatalogId { get; set; }
        public string ClientProductAlias { get; set; }

        [Required]
        public DateTime DateMin { get; set; }

        [Required]
        public DateTime DateMax { get; set; }

        public string EmployeeParticipationMessage { get; set; }

        public Boolean? IsAddedManually { get; set; }
     
        public  Good Good { get; set; }
        public  Catalog Catalog { get; set; }
    }
}

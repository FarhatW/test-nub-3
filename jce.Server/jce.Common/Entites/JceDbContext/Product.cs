using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites.JceDbContext;
using Newtonsoft.Json;

namespace jce.Common.Entites
{
    [Table("Products")]
    public class Product : Good
    {
        public string File { get; set; }
        public bool? IsDisplayedOnJCE { get; set; }
        public bool? IsBasicProduct { get; set; }
        public int? PintelSheetId { get; set; }
        public int OriginId { get; set; }

        [Required]
        [ForeignKey("Suppliers")]
        public int SupplierId { get; set; }



        public override void Add(Product component)
        {
        }

        public override void Remove(Product component)
        {
        }

        public Product()
        {
        }

        public Product(string details, int id, int price, string title, string indexId, string refPintel, string file, bool? isDisplayedOnJcen, bool? isBasicProduct,  bool isEnabled, bool isDiscountable, int goodDepartment, int productType)
            : base(id, details, price, indexId, refPintel, title, isEnabled, isDiscountable, goodDepartment, productType)
        {
            this.File = file;
            this.IsBasicProduct = isBasicProduct;
            this.IsDisplayedOnJCE = isDisplayedOnJcen;
        }
    }
}

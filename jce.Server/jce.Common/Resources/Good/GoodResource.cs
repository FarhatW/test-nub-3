using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Resources.File;
using jce.Common.Resources.Product;

namespace jce.Common.Resources.Good
{
    public class GoodResource : ResourceEntity
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string IndexId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(4)]
        public string RefPintel { get; set; }
        public bool IsDiscountable { get; set; }
        public int ProductTypeId { get; set; }
        public bool IsEnabled { get; set; }
        public string Season { get; set; }
        public bool IsBatch { get; set; }
        public int GoodDepartmentId { get; set; }

        public  GoodResource()
        {
        }

        public GoodResource(int id, string details, int price, string indexId, string refPintel, string title)
        {
            this.Id = id;
            this.Details = details;
            this.Price = price;
            this.IndexId = indexId;
            this.RefPintel = refPintel;
            this.Title = title;
        }

    }
}

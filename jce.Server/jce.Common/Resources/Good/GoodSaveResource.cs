using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;
using jce.Common.Resources.Product;

namespace jce.Common.Resources.Good
{
    public class GoodSaveResource : ResourceEntity
    {
       
        public int Id { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string IndexId { get; set; }

        [MinLength(4)]
        [Required(AllowEmptyStrings = false)]
        public string RefPintel { get; set; }
        public bool IsDiscountable { get; set; }
        public short ProductTypeId { get; set; }
        public bool IsEnabled { get; set; }
        public int GoodDepartmentId { get; set; }
        public string Season { get; set; }
        public bool IsBatch { get; set; }

        public GoodSaveResource()
        {
        }
    }
}

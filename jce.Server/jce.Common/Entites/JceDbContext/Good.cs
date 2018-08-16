using jce.Common.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace jce.Common.Entites.JceDbContext
{
    public abstract class Good : BaseEntity 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public string IndexId { get; set; } 
        [Required(AllowEmptyStrings = false)]
        [MinLength(4)]
        public string RefPintel { get; set; }
        public bool IsDiscountable { get; set; }
        public bool IsEnabled { get; set; }
        public string Season { get; set; }
        public abstract void Add(Product c);
        public abstract void Remove(Product c);

        public int GoodDepartmentId { get; set; }
        public int ProductTypeId { get; set; }


        public Good()
        {
        }

        protected Good(int id, string details, int price, string indexId, string refPintel, string title, bool isEnabled, bool isDiscountable, int goodDepartment, int productType)
        {
            this.Id = id;
            this.Details = details;
            this.Price = price;
            this.IndexId = indexId;
            this.RefPintel = refPintel;
            this.Title = title;
            this.IsEnabled = isEnabled;
            this.IsDiscountable = isDiscountable;
            this.GoodDepartmentId = goodDepartment;
            this.ProductTypeId = productType;
        }
        
    }
}

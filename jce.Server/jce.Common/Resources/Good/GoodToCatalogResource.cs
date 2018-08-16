using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Entites;
using jce.Common.Resources.Batch;
using jce.Common.Resources.File;
using jce.Common.Resources.Product;

namespace jce.Common.Resources.Good
{
    public class GoodToCatalogResource : GoodResource
    {

        public int CatalogId { get; set; }
        public string ClientProductAlias { get; set; }
        public DateTime DateMin { get; set; }
        public DateTime DateMax { get; set; }
        public string EmployeeParticipationMessage { get; set; }

        public string File { get; set; }
        public bool? IsDisplayedOnJCE { get; set; }
        public bool? IsBasicProduct { get; set; }
        public int? PintelSheetId { get; set; }

        public ICollection<ProductToBatchResource> Products { get; set; }


        public GoodToCatalogResource()
        {
            Products = new Collection<ProductToBatchResource>();
        }

        public GoodToCatalogResource(int id, string details, int price, string indexId, string refPintel, string title, int catalogId, string clientProductAlias, DateTime dateMin, DateTime dateMax, Collection<ProductResource> products)
            : base(id, details, price, indexId, refPintel, title)
        {
            this.CatalogId = catalogId;
            this.ClientProductAlias = clientProductAlias;
            this.DateMin = dateMin;
            this.DateMax = dateMax;
            //this.Products = products;

        }
    }
}

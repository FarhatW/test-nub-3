﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.Good;

namespace jce.Common.Resources
{
    public class CatalogSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        public int CeId { get; set; }
        public string IndexId { get; set; }
        public int CatalogType { get; set; }
        public bool? IsActif { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool DisplayPrice { get; set; }
        public int? ProductChoiceQuantity { get; set; }
        public int CatalogChoiceTypeId { get; set; }

        public int? BooksQuantity { get; set; }
        public int? ToysQuantity { get; set; }
        public int? SubscriptionQuantity { get; set; }

        public ICollection<GoodToCatalogSaveResource> CatalogGoods { get; set; }

        public CatalogSaveResource()
        {
            CatalogGoods = new Collection<GoodToCatalogSaveResource>();
        }
    }
}
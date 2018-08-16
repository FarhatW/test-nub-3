using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;

namespace jce.Common.Resources.Catalog
{
    public class CatalogLettersSaveResource : CatalogSaveResource
    {
        public ICollection<CatalogProductLetter> ProductLetters { get; set; }

        public CatalogLettersSaveResource()
        {
            ProductLetters = new Collection<CatalogProductLetter>();
        }
    }

}

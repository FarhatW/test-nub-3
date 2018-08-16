using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class CatalogTypeManager : ICatalogTypeManager
    {
        public List<CatalogType> GetAll()
        {
            return CatalogType.List().ToList();
        }

        public CatalogType GetItemById(int id)
        {
            return CatalogType.From(id);
        }
    }
}

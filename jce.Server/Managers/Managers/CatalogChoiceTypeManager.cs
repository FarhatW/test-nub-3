using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class CatalogChoiceTypeManager : ICatalogChoiceTypeManager
    {
        public List<CatalogChoiceType> GetAll()
        {
            return CatalogChoiceType.List().ToList();
        }

        public CatalogChoiceType GetItemById(int id)
        {
            return CatalogChoiceType.From(id);
        }
    }
}

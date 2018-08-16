using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class ProductTypeManager : IProductTypeManager
    {
        public List<ProductType> GetAll()
        {
            return ProductType.List().ToList();
        }

        public ProductType GetItemById(int id)
        {
            return ProductType.From(id);
        }
    }
}

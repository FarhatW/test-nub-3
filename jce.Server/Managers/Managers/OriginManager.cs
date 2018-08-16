using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class OriginManager : IOriginManager
    {
        public List<Origin> GetAll()
        {
            return Origin.List().ToList();
        }

        public Origin GetItemById(int id)
        {
            return Origin.From(id);
        }

        public Origin GetItemByName(string origin)
        {
            return Origin.FromName(origin);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core.EnumClasses;

namespace Managers
{
    public class GoodDepartmentManager : IGoodDepartmentManager
    {
        public List<GoodDepartment> GetAll()
        {
            return GoodDepartment.List().ToList();
        }

        public GoodDepartment GetItemById(int id)
        {
            return GoodDepartment.From(id);
        }
    }
}

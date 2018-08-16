using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using jce.Common.Core;
using jce.Common.Core.EnumClasses;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Query;
using jce.Common.Resources.Batch;
using jce.DataAccess.Core;
using Microsoft.EntityFrameworkCore;

namespace Managers
{
    public class AgeGroupManager : IAgeGroupManager
    {
        public List<AgeGroup> GetAll()
        {
            return AgeGroup.List().ToList();
        }

        public AgeGroup GetItemById(int id)
        {
            return AgeGroup.From(id);
        }

    }
}

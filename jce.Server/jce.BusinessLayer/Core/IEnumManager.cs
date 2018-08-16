using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.Common.Extentions;
using jce.Common.Resources;

namespace jce.BusinessLayer.Core
{
    public interface IEnumManager<T>
    {
        T GetItemById(int id);

        List<T> GetAll();
    }
}

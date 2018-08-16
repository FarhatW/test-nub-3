using System;
using System.Collections.Generic;
using System.Text;
using jce.BusinessLayer.Core;
using jce.Common.Core.EnumClasses;

namespace jce.BusinessLayer.IEnumManagers
{
    public interface IOriginManager : IEnumManager<Origin>
    {
        Origin GetItemByName(string origin);

    }
}

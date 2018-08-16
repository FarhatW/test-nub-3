using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Resources.Good;

namespace jce.BusinessLayer.IManagers
{
    public interface IGoodManager : IActionManager<GoodResource>
    {
        Task<GoodResource> GetItemByRefPintel(string refPintel);

    }
}

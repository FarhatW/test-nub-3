using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Resources.CeSetup;

namespace jce.BusinessLayer.IManagers
{
    public interface ICeSetupManager : IActionManager<CeSetupResource>
    {
        Task<CeSetupResource> GetByCeId(int ceId);
    }
}

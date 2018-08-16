using System.Collections.Generic;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.Common.Core;
using jce.Common.Resources;
using jce.Common.Resources.Good;
using jce.Common.Resources.PintelSheet;

namespace jce.BusinessLayer.IManagers
{
    public interface ICatalogManager : IActionManager<CatalogResource>
    {
        Task<CatalogResource> UpdatePintelSheets(int id, ResourceEntity catalogResource, List<PintelSheetResource> pintelSheetResources);
        Task<CatalogResource> UpdateLetters(int id, ResourceEntity catalogResource, List<GoodResource> pintelSheetResources);

    }
}

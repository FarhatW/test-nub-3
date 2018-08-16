using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Query;
using jce.Common.Resources;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/hisotyAction")]
    public class HisotyActionController : Controller
    {
        private readonly IHistoryActionManager _hisoryActionManager;

        public HisotyActionController(IHistoryActionManager hisoryActionManager)
        {
            _hisoryActionManager = hisoryActionManager;
        }


        public async Task<IActionResult> GetAll(HistoryActionQueryResource filterResource)
        {
            var queryResult = await _hisoryActionManager.GetAll(filterResource);

            if (queryResult == null)
                return NotFound();

            return Ok(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var historyActionResource = await _hisoryActionManager.GetItemById(id);

            if (historyActionResource == null)
                return NotFound();

            return Ok(historyActionResource);
        }

    }
}
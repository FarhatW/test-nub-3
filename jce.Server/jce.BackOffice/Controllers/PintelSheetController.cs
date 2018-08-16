using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources.PintelSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/pintelsheets")]
    public class PintelSheetController : Controller
    {
        private readonly IPintelSheetManager _pintelSheetManager;

        public PintelSheetController(IPintelSheetManager pintelSheetManager)
        {
            _pintelSheetManager = pintelSheetManager;
        }

        public async Task<IActionResult> GetAll(PintelQueryResource filterResource)
        {
            var pintelSheets = await _pintelSheetManager.GetAll(filterResource);

            if (pintelSheets == null)
            {
                return NotFound();
            }

            return Ok(pintelSheets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, PintelQueryResource filterResource = null)
        {
            var pintelSheet = await _pintelSheetManager.GetItemById(id, filterResource);

            if (pintelSheet == null)
            {
                return NotFound();
            }

            return Ok(pintelSheet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pintelSheetManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]PintelSheetSaveResource pintelSheetSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pintelSheet = await _pintelSheetManager.Update(id, pintelSheetSaveResource);

            if (pintelSheet == null)
                return NotFound();

            return Ok(pintelSheet);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PintelSheetSaveResource pintelSheetSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _pintelSheetManager.Add(pintelSheetSaveResource);

            return Ok(result);
        }
    }
}
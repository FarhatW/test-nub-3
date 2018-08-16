using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Query;
using jce.Common.Resources.CeSetup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/cesetups")]
    public class CeSetupController : Controller
    {

        private readonly ICeSetupManager _ceSetupManager;

        public CeSetupController(ICeSetupManager ceSetupManager)
        {
            _ceSetupManager = ceSetupManager;
        }

        public async Task<IActionResult> GetAll(CeSetupQueryResource filterResource)
        {
            var ceSetups = await _ceSetupManager.GetAll(filterResource);

            if (ceSetups == null)
            {
                return NotFound();
            }

            return Ok(ceSetups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CeSetupQueryResource filterResource = null)
        {
            var ceSetup = await _ceSetupManager.GetItemById(id, filterResource);

            if (ceSetup == null)
            {
                return NotFound();
            }

            return Ok(ceSetup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ceSetupManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CeSetupSaveResource ceSetupSaveResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ceSetup = await _ceSetupManager.Update(id, ceSetupSaveResource);

            if (ceSetup == null)
            {
                return NotFound();
            }

            return Ok(ceSetup);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CeSetupSaveResource ceSetupSaveResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ceSetupManager.Add(ceSetupSaveResource);

            return Ok(result);
        }

        [HttpGet("byCe/{ceId}")]
        public async Task<IActionResult> GetByCeId(int ceId)
        {
            var ceSetup = await _ceSetupManager.GetByCeId(ceId);

            if (ceSetup == null)
            {
                return NotFound();
            }

            return Ok(ceSetup);
        }


    }
}
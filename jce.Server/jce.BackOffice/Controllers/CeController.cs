using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using jce.Common.Resources.CE;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/ces")]
    public class CeController : Controller
    {
        private readonly ICeManager _ceManager;

        public CeController(ICeManager ceManager)
        {
            _ceManager = ceManager;
        }

        public async Task<IActionResult> GetAll(CeQueryResource filterResource)
        {
            var ces = await _ceManager.GetAll(filterResource);

            if (ces == null)
            {
                return NotFound();
            }

            return Ok(ces);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CeQueryResource filterResource = null)
        {
             var ce = await _ceManager.GetItemById(id, filterResource);

            if (ce == null)
            {
                return NotFound();
            }

            return Ok(ce);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ceManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CeSaveResource ceSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ce = await _ceManager.Update(id, ceSaveResource);

            if (ce == null)
                return NotFound();

            return Ok(ce);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CeSaveResource ceSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ceManager.Add(ceSaveResource);

            return Ok(result);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources.Batch;
using jce.Common.Resources.Good;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/goods")]
    public class GoodController : Controller
    {
        private readonly IGoodManager _goodManager;
        private readonly IBatchManager _batchManager;

        public GoodController(IGoodManager goodManager, IBatchManager batchManager)
        {
            _goodManager = goodManager;
            _batchManager = batchManager;
        }
       
        public async Task<IActionResult> GetAll(GoodQueryResource filterResource)
        {
            var goods = await _goodManager.GetAll(filterResource);

            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, GoodQueryResource filterResource = null)
        {
            var good = await _goodManager.GetItemById(id, filterResource);

            if (good == null)
            {
                return NotFound();
            }

            return Ok(good);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _goodManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]GoodSaveResource goodSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var good = await _goodManager.Update(id, goodSaveResource);

            if (good == null)
                return NotFound();

            return Ok(good);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]GoodSaveResource goodSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _goodManager.Add(goodSaveResource);

            return Ok(result);
        }

        [HttpGet("refpintel/{refPintel}")]
        public async Task<IActionResult> GetByRefPintel(string refPintel)
        {
            var product = await _goodManager.GetItemByRefPintel(refPintel);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}

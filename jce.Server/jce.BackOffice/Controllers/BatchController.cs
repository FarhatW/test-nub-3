using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Entites;
using jce.Common.Query;
using jce.Common.Resources.Batch;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/batches")]
    public class BatchController : Controller
    {
        private readonly IBatchManager _batchManager;
        private readonly IGoodManager _goodManager;

        public BatchController(IBatchManager batchManager, IGoodManager goodManager)
        {
            _batchManager = batchManager;
            _goodManager = goodManager;
        }


        public async Task<IActionResult> GetAll(BatchQueryResource filterResource)
        {
            var batches = await _batchManager.GetAll(filterResource);

            if (batches == null)
            {
                return NotFound();
            }

            return Ok(batches);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, BatchQueryResource filterResource = null)
        {
            var batch = await _batchManager.GetItemById(id, filterResource);

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _batchManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]BatchSaveResource batchSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var batch = await _batchManager.Update(id, batchSaveResource);

            if (batch == null)
                return NotFound();

            if (batch.DeletedProds.Count > 0)
            {
                foreach (var prodId in batch.DeletedProds)
                {
                  await _goodManager.Delete(prodId);
                }
            }

            return Ok(batch);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]BatchSaveResource batchSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _batchManager.Add(batchSaveResource);

            return Ok(result);
        }

        [HttpPost("multiAdd/")]
        public async Task<IActionResult> Add([FromBody]BatchSaveResource[] batchSaveResourceAray)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _batchManager.MultipleAdd(batchSaveResourceAray);

            return Ok(result);
        }
    }
}

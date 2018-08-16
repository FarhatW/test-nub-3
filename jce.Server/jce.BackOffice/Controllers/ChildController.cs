using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using jce.Common.Resources.Child;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/Children")]
    public class ChildController : Controller
    {
        private readonly IChildManager _childManager;

        public ChildController(IChildManager childManager)
        {
            _childManager = childManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetChilds(FilterResource filtesQuery)
        {
            var children = await _childManager.GetAll(filtesQuery);
            if (children == null)
                return NotFound();

            return Ok(children);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChildById(int id)
        {
            var child = await _childManager.GetItemById(id);
            if (child == null)
                return NotFound();

            return Ok(child);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(int id)
        {
            await _childManager.Delete(id);

            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChild([FromBody]ChildSaveResource childResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _childManager.Add(childResource);
            return Ok(result);
        }

    }
}
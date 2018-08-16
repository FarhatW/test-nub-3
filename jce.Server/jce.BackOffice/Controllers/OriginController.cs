using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/origins")]
    public class OriginController : Controller
    {
        private readonly IOriginManager _originManager;

        public OriginController(IOriginManager originManager)
        {
            _originManager = originManager;
        }

        public IActionResult GetAll()
        {
            var origins = _originManager.GetAll();
            if (origins == null)
            {
                return NotFound();
            }

            return Ok(origins);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var origin = _originManager.GetItemByName(name);

            if (origin == null)
            {
                return NotFound();
            }

            return Ok(origin);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var origin = _originManager.GetItemById(id);

            if (origin == null)
            {
                return NotFound();
            }

            return Ok(origin);
        }
    }
}

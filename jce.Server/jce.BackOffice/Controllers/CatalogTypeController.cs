using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/catalogtypes")]
    public class CatalogTypeController : Controller
    {

        private readonly ICatalogTypeManager _catalogTypeManager;

        public CatalogTypeController(ICatalogTypeManager catalogTypeManager)
        {
            _catalogTypeManager = catalogTypeManager;
        }

        public IActionResult GetAll()
        {
            var catalogTypes = _catalogTypeManager.GetAll();
            if (catalogTypes == null)
            {
                return NotFound();
            }

            return Ok(catalogTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var catalogType = _catalogTypeManager.GetItemById(id);

            if (catalogType == null)
            {
                return NotFound();
            }

            return Ok(catalogType);
        }
        
    }
}

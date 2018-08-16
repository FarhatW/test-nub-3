using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/catalogChoiceTypes")]
    public class CatalogChoiceTypeController : Controller
    {

        private readonly ICatalogChoiceTypeManager _catalogChoiceTypeManager;

        public CatalogChoiceTypeController(ICatalogChoiceTypeManager catalogChoiceTypeManager)
        {
            _catalogChoiceTypeManager = catalogChoiceTypeManager;
        }

        public IActionResult GetAll()
        {
            var catalogChoiceTypes = _catalogChoiceTypeManager.GetAll();
            if (catalogChoiceTypes == null)
            {
                return NotFound();
            }

            return Ok(catalogChoiceTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var catalogChoiceType = _catalogChoiceTypeManager.GetItemById(id);

            if (catalogChoiceType == null)
            {
                return NotFound();
            }

            return Ok(catalogChoiceType);
        }

    }
}

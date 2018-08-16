using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/producttypes")]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeManager _productTypeManager;

        public ProductTypeController(IProductTypeManager productTypeManager)
        {
            _productTypeManager = productTypeManager;
        }

        public IActionResult GetAll()
        {
            var productTypes = _productTypeManager.GetAll();
            if (productTypes == null)
            {
                return NotFound();
            }

            return Ok(productTypes);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var productType = _productTypeManager.GetItemById(id);

            if (productType == null)
            {
                return NotFound();
            }

            return Ok(productType);
        }
    }
}

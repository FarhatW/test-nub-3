using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Query;
using jce.Common.Resources.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/suppliers")]
    public class SupplierController : Controller
    {
        private readonly ISupplierManager _supplierManager;

        public SupplierController(ISupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public async Task<IActionResult> GetAll(SupplierQueryResource filterResource)
        {
            var Suppliers = await _supplierManager.GetAll(filterResource);

            if (Suppliers == null)
            {
                return NotFound();
            }

            return Ok(Suppliers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, SupplierQueryResource filterResource = null)
        {
            var Supplier = await _supplierManager.GetItemById(id, filterResource);

            if (Supplier == null)
            {
                return NotFound();
            }

            return Ok(Supplier);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SupplierSaveResource supplierSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Supplier = await _supplierManager.Update(id, supplierSaveResource);

            if (Supplier == null)
                return NotFound();

            return Ok(Supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]SupplierSaveResource supplierSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _supplierManager.Add(supplierSaveResource);

            return Ok(result);
        }
    }
}

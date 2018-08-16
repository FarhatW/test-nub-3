using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using jce.Common.Resources.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }


        public async Task<IActionResult> GetAll(ProductQueryResource filterResource)
        {
            var products = await _productManager.GetAll(filterResource);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productManager.GetItemById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("refpintel/{refPintel}")]
        public async Task<IActionResult> GetByRefPintel(string refPintel)
        {
            var product = await _productManager.GetItemByRefPintel(refPintel);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]ProductSaveResource productSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productManager.Update(id, productSaveResource);

            if (product == null)
                return NotFound();

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductSaveResource productSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productManager.Add(productSaveResource);

            return Ok(result);
        }

        [HttpPost("multiAdd/")]
        public async Task<IActionResult> Add([FromBody]ProductSaveResource[] productSaveResourceArray)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productManager.MultipleAdd(productSaveResourceArray);

            return Ok(result);
        }
    }
}
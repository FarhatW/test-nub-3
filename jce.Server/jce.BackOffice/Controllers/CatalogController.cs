using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using jce.Common.Resources.Catalog;
using jce.Common.Resources.Good;
using jce.Common.Resources.PintelSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/catalogs")]
    public class CatalogController : Controller
    {
        private readonly ICatalogManager _catalogManager;
        private readonly IGoodManager _goodManager;
        private readonly IPintelSheetManager _pintelSheetManager;

        public CatalogController(ICatalogManager catalogManager, IGoodManager goodManager, IPintelSheetManager pintelSheetManager)
        {
            _catalogManager = catalogManager;
            _goodManager = goodManager;
            _pintelSheetManager = pintelSheetManager;
        }

        public async Task<IActionResult> GetAll(CatalogQueryResource filterResource)
        {
            var catalogs = await _catalogManager.GetAll(filterResource);

            if (catalogs == null)
            {
                return NotFound();
            }

            return Ok(catalogs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CatalogQueryResource filterResource = null)
        {
            var catalog = await _catalogManager.GetItemById(id, filterResource);

             if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _catalogManager.Delete(id);

            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CatalogSaveResource catalogSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _catalogManager.Add(catalogSaveResource);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CatalogSaveResource catalogSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var catalog = await _catalogManager.Update(id, catalogSaveResource);

            if (catalog == null)
                return NotFound();

            return Ok(catalog);
        }

        [HttpPut("updatePintelSheets/{id}")]
        public async Task<IActionResult> UpdatePintelSheets(int id,[FromBody]CatalogSaveResource catalogSaveResource, CatalogQueryResource catalogQuery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pintelSheets = await _pintelSheetManager.GetAll(new PintelQueryResource() { PintelSheetArray = catalogQuery.PintelSheetsArray });
            var catalog = await _catalogManager.UpdatePintelSheets(id, catalogSaveResource, pintelSheets.Items.ToList());

            if (catalog == null)
                return NotFound();

            return Ok(catalog);
        }

        [HttpPut("updateLetters/{id}")]
        public async Task<IActionResult> UpdateLetters(int id, [FromBody]CatalogLettersSaveResource catalogSaveResource, CatalogQueryResource catalogQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goods = await _goodManager.GetAll(new GoodQueryResource() { ProductIndex = catalogQuery.LettersArray });
            var catalog = await _catalogManager.UpdateLetters(id, catalogSaveResource, goods.Items.ToList());

            if (catalog == null)
                return NotFound();

            return Ok(catalog);
        }




    }
}
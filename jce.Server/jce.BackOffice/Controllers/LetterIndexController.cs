using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/letterindex")]
    public class LetterIndexController : Controller
    {
        private readonly ILetterIndexManager _letterIndexManager;

        public LetterIndexController(ILetterIndexManager letterIndexManager)
        {
            _letterIndexManager = letterIndexManager;
        }

        public IActionResult GetAll()
        {
            var letterIndexes = _letterIndexManager.GetAll();
            if (letterIndexes == null)
            {
                return NotFound();
            }

            return Ok(letterIndexes);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var letterIndex = _letterIndexManager.GetItemByName(name);

            if (letterIndex == null)
            {
                return NotFound();
            }

            return Ok(letterIndex);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            var letterIndex = _letterIndexManager.GetItemById(id);

            if (letterIndex == null)
            {
                return NotFound();
            }

            return Ok(letterIndex);
        }

      
    }
}
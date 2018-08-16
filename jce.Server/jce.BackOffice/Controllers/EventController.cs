using jce.Common.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly IEventManager _eventManager;

        public EventController(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(EventQueryResource filtesQuery)
        {
            var events = await _eventManager.GetAll(filtesQuery);
            if (events == null)
                return NotFound();


            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getEventById(int id)
        {
            var events = await _eventManager.GetItemById(id);
            if (events == null)
                return NotFound();

            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]EventResource EventResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventManager.Add(EventResource);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]EventResource EventResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventManager.Update(id, EventResource);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _eventManager.Delete(id);

            return Ok(id);
        }
    }
}

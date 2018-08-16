using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources;
using jce.Common.Resources.adminProfile;
using jce.Common.Resources.personProfile;
using jce.Common.Resources.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/users")]
    public class JceProfileController : Controller
    {

        private readonly IJceProfileManager _jceProfileManager;

        public JceProfileController(IJceProfileManager jceProfileManager)
        {
            _jceProfileManager = jceProfileManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(JceProfileQueryResource filterResource)
        {
            var queryResult = await _jceProfileManager.GetAll(filterResource);

            if (queryResult == null)
                return NotFound();

            return Ok(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userProfileResource = await _jceProfileManager.GetItemById(id);

            if (userProfileResource == null)
                return NotFound();

            return Ok(userProfileResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jceProfileManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("admin/{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody]AdminProfileSaveResource userProfileResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resource = await _jceProfileManager.UpdateAdminJceProfile(id, userProfileResource);

            if (resource == null)
                return NotFound();

            return Ok(resource);
        }

        [HttpPut("person/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody]PersonProfileSaveResource userProfileResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resource = await _jceProfileManager.UpdatePersonJceProfile(id, userProfileResource);

            if (resource == null)
                return NotFound();

            return Ok(resource);
        }

        [HttpPost("admin")]
        public async Task<IActionResult> AddAdmin([FromBody]AdminProfileSaveResource userProfileResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _jceProfileManager.AddAdminJceProfile(userProfileResource));
        }
        [HttpPost("person")]
        public async Task<IActionResult> AddPerson([FromBody]PersonProfileSaveResource userProfileResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _jceProfileManager.AddPersonJceProfile(userProfileResource));
        }
    }
}
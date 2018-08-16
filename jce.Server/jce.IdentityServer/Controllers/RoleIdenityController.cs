using System.Threading.Tasks;
using jce.BusinessLayer.IManagers;
using jce.Common.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jce.IdentityServer.Controllers
{
    [Route("api/roles")]
    [SecurityHeaders]
    public class RoleIdenityController : Controller
    {
    
        //all domaine in the same Interface
        private readonly IRoleManager _roleManager;

        public RoleIdenityController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles(FilterResource filtesQuery)
        {
            var roles = await _roleManager.GetAll(filtesQuery);
            if (roles == null)
                return NotFound();


            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesById(int id)
        {
            var role = await _roleManager.GetItemById(id);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleManager.Delete(id);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleResource roleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resource = await _roleManager.Update(id, roleResource);

            if (resource == null)
                return NotFound();

            return Ok(resource);

        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody]RoleResource roleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleManager.Add(roleResource);
            return Ok(result);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/agegroups")]
    public class AgeGroupController : Controller
    {
        private readonly IAgeGroupManager _ageGroupManager;

        public AgeGroupController(IAgeGroupManager ageGroupManager)
        {
            _ageGroupManager = ageGroupManager;
        }

        public IActionResult GetAll()
        {
            var ageGroups = _ageGroupManager.GetAll();
            if (ageGroups == null)
            {
                return NotFound();
            }

            return Ok(ageGroups);
        }

        [HttpGet("{id}")]
        public  IActionResult GetById(int id)
        {
            var ageGroup = _ageGroupManager.GetItemById(id);

            if (ageGroup == null)
            {
                return NotFound();
            }

            return Ok(ageGroup);
        }
    }
}
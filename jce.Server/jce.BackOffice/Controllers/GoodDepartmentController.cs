using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.IEnumManagers;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Produces("application/json")]
    [Route("api/gooddepartments")]
    public class GoodDepartmentController : Controller
    {
        private readonly IGoodDepartmentManager _goodDepartmentManager;

        public GoodDepartmentController(IGoodDepartmentManager goodDepartmentManager)
        {
            _goodDepartmentManager = goodDepartmentManager;
        }

        public IActionResult GetAll()
        {
            var departments = _goodDepartmentManager.GetAll();
            if (departments == null)
            {
                return NotFound();
            }

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _goodDepartmentManager.GetItemById(id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}

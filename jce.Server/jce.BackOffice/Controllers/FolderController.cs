using jce.BusinessLayer.Core;
using jce.Common.Core.File;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jce.BackOffice.Controllers
{
    [Route("/api/folders/")]
    public class FolderController : Controller
    {
        IFolderManager _folderManager;

        public FolderController(IFolderManager folderManager)
        {
            _folderManager = folderManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSystemFolders()
        {
            var result = await _folderManager.GetSystemFolder();

            if(result == null)
            {
                return null;
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] string DirectoryPath)
        {
            var result = _folderManager.Add(DirectoryPath);

            if (result == null)
            {
                return null;
            }

            return Ok(result);
        }
    }
}

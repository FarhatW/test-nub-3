using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using jce.BusinessLayer.Core;
using jce.BusinessLayer.IManagers;
using jce.Common.Core.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BackOffice.Controllers
{
    [Route("/api/files/")]
    public class FileController : Controller
    {
        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFiles(int goodId, IFormFile file)
        {
            var result = await _fileManager.GetSystemFiles();

            if(result == null)
            {
                return null;
            }

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DTONode paramDTONode)
        {
            var result = await _fileManager.Add(paramDTONode);

            if(result == null)
            {
                return null;
            }

            return Ok(result);
        }

        [HttpPost("upload/")]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest();
            }

            var form = Request.Form;
            var result = await _fileManager.Index(files, form);

            return Ok(result);
        }
    }
}

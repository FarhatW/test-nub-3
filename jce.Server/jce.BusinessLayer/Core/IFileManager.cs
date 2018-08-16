using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using jce.Common.Core.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jce.BusinessLayer.Core
{
    public interface IFileManager
    {
        Task<DTONode> GetSystemFiles();
        Task<DTONode> Add(DTONode paramDTONode);
        Task<List<string>> Index(ICollection<IFormFile> files, IFormCollection form);

    }
}

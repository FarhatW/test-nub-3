using jce.Common.Core.File;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jce.BusinessLayer.Core
{
    public interface IFolderManager
    {
        Task<DTONode> GetSystemFolder();
        string Add(string DirectoryPath);
    }
}

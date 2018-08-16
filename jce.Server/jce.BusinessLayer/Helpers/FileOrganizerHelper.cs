using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jce.BusinessLayer.Helpers
{
    public class FileOrganizerHelper
    {
        public static void WebRootPath(IHostingEnvironment host)
        {
            host.WebRootPath =
                System.IO.Path.Combine(
                    Directory.GetCurrentDirectory(),
                    @"wwwroot\Files");

            // Create wwwroot\Files directory if needed
            if (!Directory.Exists(host.WebRootPath))
            {
                DirectoryInfo di =
                    Directory.CreateDirectory(host.WebRootPath);
            }
        }
    }
}

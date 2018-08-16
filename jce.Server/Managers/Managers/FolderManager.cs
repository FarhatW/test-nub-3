using jce.BusinessLayer.Core;
using jce.BusinessLayer.Helpers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class FolderManager : IFolderManager
    {
        DTONode objDTONode;

        private ISaveHistoryActionData SaveHistoryActionData { get; }
        private IHostingEnvironment _host { get; }

        public FolderManager(ISaveHistoryActionData saveHistoryActionData, IHostingEnvironment host)
        {
            SaveHistoryActionData = saveHistoryActionData;
            _host = host;

            FileOrganizerHelper.WebRootPath(_host);

        }

        public async Task<DTONode> GetSystemFolder()
        {
            objDTONode = new DTONode();

            objDTONode.label = "TreeRoot";
            objDTONode.data = "TreeRoot";
            objDTONode.expandedIcon = "fa-folder-open";
            objDTONode.collapsedIcon = "fa-folder";
            objDTONode.children = new List<DTONode>();

            if (Directory.Exists(_host.WebRootPath))
            {
                DTONode objNewDTONode = new DTONode();

                objNewDTONode.label = "JCEBase";
                objNewDTONode.data = @"\";
                objNewDTONode.expandedIcon = "fa-folder-open";
                objNewDTONode.collapsedIcon = "fa-folder";
                objNewDTONode.children = new List<DTONode>();

                objDTONode.children.Add(objNewDTONode);

                // Get Folders
                ProcessDirectory(_host.WebRootPath, 0);
            }

            return objDTONode;
        }

        public string Add(string DirectoryPath)
        {

            var dictionary = new Dictionary<string, string>();

            if (DirectoryPath.IndexOf("\\\\") == 0)
            {
                DirectoryPath = DirectoryPath.Replace("\\\\", "");
            }
            // Create path
            string NewPath = Path.Combine(_host.WebRootPath, DirectoryPath);
            if (!Directory.Exists(NewPath))
            {
                Directory.CreateDirectory(NewPath);
                dictionary.Add("path", NewPath);
                var newPathJson = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

                return newPathJson;
            }
            dictionary.Add("path", DirectoryPath);

            var json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            return json;
        }

        public void ProcessDirectory(string targetDirectory, int paramNumberOfDots)
        {
            paramNumberOfDots++;
            string WebRootPath = _host.WebRootPath + @"\";
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                // The directory label should only contain the name of the directory
                string subdirectoryLabel = FixDirectoryName(subdirectory);

                DTONode objNewDTONode = new DTONode();

                objNewDTONode.data = subdirectory.Replace(WebRootPath, "");
                objNewDTONode.expandedIcon = "fa-folder-open";
                objNewDTONode.collapsedIcon = "fa-folder";
                objNewDTONode.children = new List<DTONode>();

                // Update the label to add dots in front of the name
                objNewDTONode.label = $"{AddDots(paramNumberOfDots)}{subdirectoryLabel}";

                objDTONode.children.Add(objNewDTONode);

                ProcessDirectory(subdirectory, paramNumberOfDots);
            }
        }

        private string FixDirectoryName(string subdirectory)
        {
            string subdirectoryLabel = subdirectory;

            // Create a subdirectory label that does not include the path Root
            int intRootPosition = _host.WebRootPath.Count() + 1;
            subdirectoryLabel =
                subdirectory.Substring(intRootPosition,
                (subdirectory.Length - intRootPosition));

            // Create a subdirectory label that does not include the parent
            int intParentPosition = subdirectoryLabel.LastIndexOf(@"\");

            if (intParentPosition > 0)
            {
                intParentPosition++;
                subdirectoryLabel =
                    subdirectoryLabel.Substring(intParentPosition,
                    (subdirectoryLabel.Length - intParentPosition));
            }
            return subdirectoryLabel;
        }

        private static string AddDots(int intDots)
        {
            String strDots = "";
            for (int i = 0; i < intDots; i++)
            {
                strDots += ". ";
            }
            return strDots;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using jce.BusinessLayer.Core;
using jce.BusinessLayer.Helpers;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Core;
using jce.Common.Entites.JceDbContext;
using jce.Common.Core.File;
using jce.Common.Resources;
using jce.Common.Resources.File;
using jce.Common.Resources.Good;
using jce.Common.Resources.Product;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using jce.Common.Core.EnumClasses;
using System.Net.Http.Headers;

namespace Managers
{
    public class FileManager : IFileManager
    {

        //DTONode objDTONode;
        //private Dictionary<string, List<string>> returnDictionary;

        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }
        private  IHostingEnvironment _host { get; }
        private IRepository<JceDbContext> Repository { get; }

        public FileManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork,
            IMapper mapper, IHostingEnvironment host)
        {
            SaveHistoryActionData = saveHistoryActionData;
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;
            _host = host;

            FileOrganizerHelper.WebRootPath(_host);

        }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public async Task SaveHistoryAction(string action, ResourceEntity fileResourceEntity)
        {
            var resource = new HistoryActionResource
            {
                ActionName = action,
                Content = JsonConvert.SerializeObject(fileResourceEntity),
                UserId = fileResourceEntity.UpdatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = fileResourceEntity.UpdatedBy,
                UpdatedBy = fileResourceEntity.UpdatedBy,
                TableName = "file"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task<List<string>> Index(ICollection<IFormFile> files, IFormCollection form)
        {
            var fileNameList = new List<string>();

            // Retrieve SelectedFolder
            string SelectedFolder = form["selectedFolder"].First();

            var dictionaryReturn = new Dictionary<string, Dictionary<string, string>>();

            // Process all Files
            foreach (var file in form.Files)
            {
                var fileSize = file.Length / 1024;
                var checkMime = MimeTypes.GetContentType(file.FileName);
                var validFile = Folder.FromName(SelectedFolder).AcceptedMimes.Contains(checkMime)
                    && fileSize < Folder.FromName(SelectedFolder).MaxFileSize;

                if (validFile)
                {
                    using (var readStream = file.OpenReadStream())
                    {
                        var filename = ContentDispositionHeaderValue
                                                .Parse(file.ContentDisposition)
                                                .FileName
                                                .Trim('"');

                        filename = _host.WebRootPath + $@"\{SelectedFolder}\{filename}";

                        //Save file to harddrive
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            await file.CopyToAsync(fs);
                            await fs.FlushAsync();
                        }

                        fileNameList.Add(filename);
                    }
                }
                else
                {

                }
            }
            return fileNameList;
        }

        public void AddValidFile(string filename)
        {
            var successString = String.Format("{0} a bien été ajouté", filename);
        }


        public async Task<DTONode> GetSystemFiles()
        {
            DTONode objDTONode = new DTONode();

            if (Directory.Exists(_host.WebRootPath))
            {
                objDTONode.label = "Root";
                objDTONode.data = "Root";
                objDTONode.expandedIcon = "fa-folder-open";
                objDTONode.collapsedIcon = "fa-folder";
                objDTONode.children = new List<DTONode>();

                ProcessDirectory(_host.WebRootPath, ref objDTONode);
            }

            return objDTONode;
        }

        public async Task<DTONode> Add(DTONode paramDTONode)
        {

            foreach(var objDTONode in paramDTONode.children)
            {
                if(objDTONode.expandedIcon == "fa-folder-open")
                {
                    DeleteFolder(objDTONode);
                }
                else
                {
                    DeleteFile(objDTONode);
                }
            }

            return paramDTONode;
        }

        private void DeleteFolder(DTONode objDTONode)
        {
            try
            {
                // Create path
                string FullPath = Path.Combine(_host.WebRootPath, objDTONode.data);
                if (Directory.Exists(FullPath))
                {
                    Directory.Delete(FullPath, true);
                }
            }
            catch
            {
                // Do nothing 
            }
        }

        private void DeleteFile(DTONode objDTONode)
        {
            try
            {
                // Create path
                string FullPath = Path.Combine(_host.WebRootPath, objDTONode.data);
                if (System.IO.File.Exists(FullPath))
                {
                    System.IO.File.Delete(FullPath);
                }
            }
            catch
            {
                // Do nothing 
            }
        }


        public void ProcessDirectory(string targetDirectory, ref DTONode paramDTONode)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName, ref paramDTONode);
            }
            // subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                string WebRootPath = _host.WebRootPath + @"\";
                // The directory label should only contain the name of the directory
                string subdirectoryLabel = FixDirectoryName(subdirectory);
                DTONode objDTONode = new DTONode();
                objDTONode.label = subdirectoryLabel;
                objDTONode.data = subdirectory.Replace(WebRootPath, "");
                objDTONode.expandedIcon = "fa-folder-open";
                objDTONode.collapsedIcon = "fa-folder";
                objDTONode.children = new List<DTONode>();
                objDTONode.type = "folder";
                paramDTONode.children.Add(objDTONode);
                ProcessDirectory(subdirectory, ref objDTONode);
            }
        }

        public void ProcessFile(string path, ref DTONode paramDTONode)
        {
            string WebRootPath = _host.WebRootPath + @"\";
            string FileName = Path.GetFileName(path);
            string FilePath = path;
            DTONode objDTONode = new DTONode();
            objDTONode.label = FileName;
            objDTONode.data = FilePath.Replace(WebRootPath, "");
            objDTONode.expandedIcon = "fa-file";
            objDTONode.collapsedIcon = "fa-file";
            objDTONode.type = "file";
            paramDTONode.children.Add(objDTONode);
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
    }
}

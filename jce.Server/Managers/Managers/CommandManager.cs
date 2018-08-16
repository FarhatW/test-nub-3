using AutoMapper;
using jce.BusinessLayer.SaveData;
using jce.DataAccess.Core;
using jce.BusinessLayer.IManagers;
using jce.Common.Core;
using jce.Common.Extentions;
using jce.Common.Resources;
using System.Threading.Tasks;
using jce.Common.Entites;
using System.Linq;
using System;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using jce.Common.Query;
using System.Linq.Expressions;
using System.Collections.Generic;
using jce.DataAccess.Core.dbContext;

namespace Managers
{
    public class CommandManager : ICommandManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }


        private IRepository<JceDbContext> Repository { get; }

        public CommandManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
        {
            SaveHistoryActionData = saveHistoryActionData;
            UnitOfWork = unitOfWork;
            _mapper = mapper;
            Repository = repository;
        }

        public async Task SaveChanges()
        {
            await UnitOfWork.SaveIntoJceDbContextAsync();
        }

        public async Task SaveHistoryAction(string action, ResourceEntity userProfile)
        {
            var resource = new HistoryActionResource
            {
                ActionName = action,
                Content = JsonConvert.SerializeObject(userProfile),
                UserId = userProfile.UpdatedBy,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                CreatedBy = userProfile.UpdatedBy,
                UpdatedBy = userProfile.UpdatedBy,
                TableName = "Command"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CommandResource> GetItemById(int id, FilterResource filterResource = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CommandResource> Add(ResourceEntity resourceEntity)
        {
            var saveCommand = (CommandSaveResource)resourceEntity;

            if (ExistChildIdAndProductId(saveCommand.CommandChildProduct.FirstOrDefault().ChildId, saveCommand.CommandChildProduct.FirstOrDefault().ProductId))
            {
                throw new Exception("schedules already exist in this CE");
            }

            //idce
            int idce = 1;
            //Verifier les configurations CE pour Overtake Salarié
            saveCommand.OvertakeCommand = await OvertakeCommand(idce, saveCommand.CommandChildProduct);

            var commandMap = _mapper.Map<CommandSaveResource, Command>(saveCommand);

            Repository.Add(commandMap);

            await SaveChanges();
            await SaveHistoryAction("Add", saveCommand);

            string test = "lol";
            Task<int> retour = new SendMail().SendMailAsync(test);
            return _mapper.Map<Command, CommandResource>(commandMap);
        }

        public async Task<int> OvertakeCommand(int IdCe, ICollection<CommandChildProduct> CommandChildProduct)
        {
            var CeSetup = await Repository.GetOne<CeSetup>().FirstOrDefaultAsync(v => v.CeId == IdCe);

            int OvertakeTotal = 0;
            foreach (var CommandLine in CommandChildProduct)
            {
                //MultiConfig possible
                if (CeSetup.IsExceeding == true)
                {
                    if (CeSetup.CeCalculation == true)
                    {
                        OvertakeTotal += CommandLine.OvertakeCommandChild;
                    }
                else if (CeSetup.ChildCalculation == true) {
                        if (CommandLine.OvertakeCommandChild >= 0)
                        {
                            OvertakeTotal += CommandLine.OvertakeCommandChild;
                        }
                    }
                }
            }
            return OvertakeTotal;
        }

        public bool ExistChildIdAndProductId(int ChildId, int ProductId)
        {
            var CommandChildProduct = Repository.GetAll<CommandChildProduct>();
            var existChildId = CommandChildProduct.Any(x => string.Equals(x.ChildId, ChildId));
            var existProductId = CommandChildProduct.Any(x => string.Equals(x.ProductId, ProductId));

            return CommandChildProduct != null && existChildId == true && existProductId == true;
        }

        public Task<CommandResource> Update(int id, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<CommandResource>> GetAll(FilterResource filterResource)
        {
            throw new NotImplementedException();
        }
    }
}
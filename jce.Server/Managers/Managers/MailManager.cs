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
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.CeSetup;
using jce.DataAccess.Core.dbContext;

namespace Managers
{
    public class MailManager : IMailManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }


        private IRepository<JceDbContext> Repository { get; }

        public MailManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                TableName = "Mail"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MailResource> GetItemById(int id, FilterResource filterResource = null)
        {
            throw new NotImplementedException();
        }

        public async Task<MailResource> Add(ResourceEntity resourceEntity)
        {
            var saveMail = (MailSaveResource)resourceEntity;
            var mailMap = _mapper.Map<MailSaveResource, Mail>(saveMail);

            //ScheduleVerifExistAdd(saveSchedule.ScheduleMin, saveSchedule.ScheduleMax, saveSchedule.EventId);


            Repository.Add(mailMap);
            await SaveChanges();
            await SaveHistoryAction("Add", saveMail);

            return _mapper.Map<Mail, MailResource>(mailMap);
        }

        public async Task<string> MailTreatment(int idPersonne, string typeMail, MailInCeSetup mailPersonnaliser)
        {         
            var personne = await Repository.GetOne<PersonJceProfile>()
                .FirstOrDefaultAsync(v => v.Id == idPersonne);

            var ce = await Repository.GetOne<Ce>()
                .FirstOrDefaultAsync(v => v.Id == personne.CeId);

            var objectMail = new MailInCeSetup();

            if (objectMail == null)
            {
                var cesetup = await Repository.GetOne<CeSetup>()
                    .Include(c => c.Mail)
                    .FirstOrDefaultAsync(v => v.Id == personne.CeId);

                var result = _mapper.Map<CeSetup, CeSetupResource>(cesetup);

               // objectMail = result.MailInCeSetup.Where(m => m.MailType == typeMail).FirstOrDefault();
                if (objectMail == null)
                    throw new Exception("TypeMail Inconnu");
            } else
            {
                objectMail = mailPersonnaliser;
            }

            switch (typeMail)
            {
                case "bienvenue":
                    MailBienvenue(objectMail, personne, ce);
                    break;
                case "bienvenueResp":
                    //MailResponsable(objectMail);
                    break;
                case "commande":
                    //MailCommande(objectMail);
                    break;
                case "retardataire":
                    //MailRetardataire(objectMail);
                    break;
                default:
                    var test = "aucun mail recoonu";
                    break;
            }


            //Task<int> retour = new SendMail().SendMailAsync(mailObject, mailBody, mailPersonne);
            return "MailEnvoyé";
        }

        public MailInCeSetup MailBienvenue(MailInCeSetup objectMail, PersonJceProfile personne, Ce ce)
        {
            var MailObject = objectMail.MailObject;
            MailObject = MailObject.Replace("##CE##", ce.Name);

            var mailBody = objectMail.MailBody;

            mailBody = mailBody.Replace("##CE##", ce.Name);
            mailBody = mailBody.Replace("##IDENTIFIANT##", "en attente du SSO");
            mailBody = mailBody.Replace("##PASSWORD##", "en attente du SSO");
            mailBody = mailBody.Replace("##NOM_RESPONSABLE##", "En attente de création");

            objectMail.MailBody = mailBody;
            objectMail.MailObject = MailObject;
            return objectMail;
        }

        public Task<MailResource> Update(int id, ResourceEntity resourceEntity)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<MailResource>> GetAll(FilterResource filterResource)
        {
            throw new NotImplementedException();
        }
    }
}
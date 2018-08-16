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
using jce.DataAccess.Core.dbContext;

namespace Managers
{
    public class ScheduleManager : IScheduleManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }


        private IRepository<JceDbContext> Repository { get; }

        public ScheduleManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                TableName = "Schedule"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task<ScheduleResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var schedules = await Repository.GetOne<Schedule>()
                .Include(s => s.EventSchedulesEmployees)
                .ThenInclude(ese => ese.Employee)
                .FirstOrDefaultAsync(v => v.Id == id);
            return _mapper.Map<Schedule, ScheduleResource>(schedules);
        }

        public async Task<ScheduleResource> Add(ResourceEntity resourceEntity)
        {
            var saveSchedule = (ScheduleSaveResource)resourceEntity;
            var schedulemap = _mapper.Map<ScheduleSaveResource, Schedule>(saveSchedule);

            ScheduleVerifExistAdd(saveSchedule.ScheduleMin, saveSchedule.ScheduleMax, saveSchedule.EventId);

            Repository.Add(schedulemap);
            await SaveChanges();
            await SaveHistoryAction("Add", saveSchedule);

            return _mapper.Map<Schedule, ScheduleResource>(schedulemap);
        }

        public void ScheduleVerifExistAdd(DateTime ScheduleMin, DateTime ScheduleMax, int EventId)
        {
            var schedules = Repository.GetAll<Schedule>()
                .Where(s => s.EventId == EventId && s.ScheduleMin == ScheduleMin && s.ScheduleMax == ScheduleMax);

            if (schedules != null && schedules.Count() > 0)
                throw new Exception("schedules already exist in this CE");
        }

        public async Task<ScheduleResource> Update(int id, ResourceEntity resourceEntity)
        {
            var schedules = await Repository.GetOne<Schedule>().FirstOrDefaultAsync(v => v.Id == id);

            if (schedules == null)
                throw new Exception("schedules not Found");

            var scheduleSaveResource = (ScheduleSaveResource)resourceEntity;
            //scheduleSaveResource.Id = id;
            _mapper.Map(scheduleSaveResource, schedules);

            //Shedules Config
            await ScheduleConfig(scheduleSaveResource);

            //remapper le nombre de participant restant à l'événement après décrémentation
            _mapper.Map(scheduleSaveResource, schedules);

            schedules.UpdatedOn = DateTime.Now;
            await SaveChanges();
            await SaveHistoryAction("Update", scheduleSaveResource);

            var result = await GetItemById(schedules.Id);
            return result;
        }

        public async Task ScheduleConfig(ScheduleSaveResource scheduleSaveResource)
        {
            //Maj Schedules sans les inscriptions
            if (scheduleSaveResource.EventSchedulesEmployees.Count() == 0)
            {
                VerifExistScheduleUpdate(scheduleSaveResource.ScheduleMin, scheduleSaveResource.ScheduleMax, scheduleSaveResource.EventId, scheduleSaveResource.NbParticipant, scheduleSaveResource.Id, scheduleSaveResource.IsDelete);
            }
            else
            {
                //Verifier que le user n'est pas déjà enregistré
                await UserExistInSchedulesEmployee(scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().EmployeeId);
                //Verifier que IdCe de l'événement est le même que celui de l'utilisateur
                CEIdSameEmployeeId(scheduleSaveResource);
                //Verifier que le nombre d'enfant rentré correspond au nombre d'enfant de l'employé ayant la possibilité de participer à l'événement (age min - age max)
                //Verifier egalement que le nombre de personne rentré n'est pas supérieur aux nombres de personnes dans la configuration
                await VerifNbChildAndNbAdult(scheduleSaveResource);
                //Verifier que le nombre de personne selectionne est superieur au nombre de personne restante
                //decremente le nombre de personne selectionne au nombre de personne restante
                VerifAndDeCrementNbPart(scheduleSaveResource);
            }
        }

        public async Task VerifNbChildAndNbAdult(ScheduleSaveResource scheduleSaveResource)
        {
            var ev = Repository.GetOne<Event>().FirstOrDefault(e => e.Id == scheduleSaveResource.EventId);
            //--- Verif NbChild ---
            var children = await Repository.GetAll<Child>().AsQueryable().Where(e => e.PersonJceProfileId == scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().EmployeeId).ToListAsync();
            int countChildEvent = 0;
            foreach (var i in children)
            {
                var childDateTime = Convert.ToDateTime(i.BirthDate);
                var childAge = DateTime.Now.Year - childDateTime.Year - (DateTime.Now.Month < childDateTime.Month ? 1 : DateTime.Now.Day < childDateTime.Day ? 1 : 0);

                if (childAge <= ev.MaxAge && childAge >= ev.MinAge)
                    countChildEvent = countChildEvent + 1;
            }

            if (countChildEvent == 0 )
                throw new Exception("Aucun de vos enfant ne peut participer à l'evenement");

            if (scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().NbChildren > countChildEvent)
                throw new Exception("Vous N'avez pas la possibilité d'avoir autant de billet pour vos enfants");

            // ---- Verification sur le nbAdulte ---
            var nbAdulteSelect = scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().NbParticipantsEvent - scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().NbChildren;
            if (ev.AdultNumber < nbAdulteSelect)
                throw new Exception("Vous ne pouvez pas selectionner autant de personne");
        }

        public void VerifAndDeCrementNbPart(ScheduleSaveResource scheduleSaveResource)
        {
            // --- verifie que le nombre de participant restant est superieur au nombre de participant event
            if (scheduleSaveResource.NbParticipant < scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().NbParticipantsEvent)
                throw new Exception("Plus assez de place");

            // Decremente
            scheduleSaveResource.NbParticipant = scheduleSaveResource.NbParticipant - scheduleSaveResource.EventSchedulesEmployees.FirstOrDefault().NbParticipantsEvent;

        }

            public void CEIdSameEmployeeId(ScheduleSaveResource scheduleSaveResource)
        {
            var ev = Repository.GetOne<Event>().FirstOrDefault(e => e.Id == scheduleSaveResource.EventId);
            if (ev == null)
                throw new Exception("event not Found");

//            if (scheduleSaveResource.EmployeeCeId != ev.CeId)
//                throw new Exception("Personne n'appartenant pas au CE de cet event");
        }

        public void VerifExistScheduleUpdate(DateTime ScheduleMin, DateTime ScheduleMax, int EventId, int NbParticipant, int IdSchedule, bool IsDelete)
        {
            var schedules = Repository.GetOne<Schedule>()
                .Where(s => s.EventId == EventId && s.Id == IdSchedule);

            if (schedules != null && schedules.Count() == 0)
            {
                throw new Exception("schedule can not exist is this CE");
            }

            schedules = schedules.Where(s => s.ScheduleMin == ScheduleMin && s.ScheduleMax == ScheduleMax && s.NbParticipant == NbParticipant && s.IsDelete == IsDelete);

            if (schedules != null && schedules.Count() > 0)
            {
                throw new Exception("nothing change had detected");
            }
        }

        public async Task UserExistInSchedulesEmployee(int id)
        {
            var ShceduleEmployee = await Repository.GetAll<ScheduleEmployee>().AsQueryable().Where(se => se.EmployeeId == id).ToListAsync();

            if (ShceduleEmployee.Count() > 0)
            {
                throw new NotImplementedException("Inscription déjà enregistré");
            }
        }

        public async Task<QueryResult<ScheduleResource>> GetAll(FilterResource filterResource)
        {
            var queryFilterResource = (ScheduleQueryResource)filterResource;
            var result = new QueryResult<Schedule>();
            var filters = _mapper.Map<ScheduleQueryResource, ScheduleQuery>(queryFilterResource);
            var queryObj = filters;

            var query = Repository.GetAll<Schedule>().Include(s => s.EventSchedulesEmployees).ThenInclude(ese => ese.Employee).AsQueryable();
            if (filterResource != null)
            {
                if (filters.EventId.HasValue)
                {
                    query = query.Where(x => x.EventId == filters.EventId);
                }
            }
            var columnMap = new Dictionary<string, Expression<Func<Schedule, object>>> { };

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Schedule>, QueryResult<ScheduleResource>>(result);
        }

        public async Task Delete(int id)
        {
            var schedules = await Repository.GetOne<Schedule>().FirstOrDefaultAsync(v => v.Id == id);

            if (schedules == null)
                throw new Exception("schedule not Found");

            var resource = _mapper.Map<Schedule, ScheduleSaveResource>(schedules);

            Repository.Remove(schedules);
            //schedules.IsDelete = true;
            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }
    }
}
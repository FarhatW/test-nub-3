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
using System.Collections.Generic;
using System.Linq.Expressions;
using jce.DataAccess.Core.dbContext;

namespace Managers
{
    public class EventManager : IEventManager
    {
        private ISaveHistoryActionData SaveHistoryActionData { get; }

        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork { get; }


        private IRepository<JceDbContext> Repository { get; }

        public EventManager(IRepository<JceDbContext> repository, ISaveHistoryActionData saveHistoryActionData, IUnitOfWork unitOfWork, IMapper mapper)
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
                TableName = "event"
            };

            await SaveHistoryActionData.SaveHistory(resource);
        }

        public async Task<EventResource> GetItemById(int id, FilterResource filterResource = null)
        {
            var events = await Repository.GetOne<Event>().
                Include(e => e.Schedules).ThenInclude(s => s.EventSchedulesEmployees)
                .ThenInclude(ese => ese.Employee)
                .FirstOrDefaultAsync(v => v.Id == id);

            return _mapper.Map<Event, EventResource>(events);
        }

        public async Task<QueryResult<EventResource>> GetAll(FilterResource filterResource)
        {
            var queryFilterResource = (EventQueryResource)filterResource;
            var result = new QueryResult<Event>();
            var filters = _mapper.Map<EventQueryResource, EventQuery>(queryFilterResource);
            var queryObj = filters;

            var query = Repository.GetAll<Event>().Include(e => e.Schedules).ThenInclude(s => s.EventSchedulesEmployees)
                .ThenInclude(ese => ese.Employee).AsQueryable();
            if (filterResource != null)
            {
                 if (filters.CeId.HasValue)
                {
                    query = query.Where(x => x.CeId == filters.CeId);
                } 
            }
            var columnMap = new Dictionary<string, Expression<Func<Event, object>>> { };

            query = query.ApplyOrdering(queryObj, columnMap);
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            result.TotalItems = await query.CountAsync();

            return _mapper.Map<QueryResult<Event>, QueryResult<EventResource>>(result);
        }

        /// <summary>
        /// Verifie que le nom envoyé n'existe pas déjà pour le CE donné
        /// </summary>
        /// <param name="eventname"></param>
        /// <param name="ceid"></param>
        /// <returns>Retourne true (nom existe) ou false (nom n'existe pas)</returns>
        public bool EventExist(string eventname, int ceid)
        {
            var events = Repository.GetAll<Event>();
            var existEventName = events.Any(x => string.Equals(x.Title, eventname, StringComparison.CurrentCultureIgnoreCase));
            var existEventCeId = events.Any(x => string.Equals(x.CeId, ceid));

            return events != null && existEventName == true && existEventCeId == true;
        }

        public async Task<EventResource> Add(ResourceEntity resourceEntity)
        {
            var saveEvent = (EventResource)resourceEntity;

            if (EventExist(saveEvent.Title, saveEvent.CeId))
            {
                throw new Exception("Event already exist in this CE");
            }
            var eventmap = _mapper.Map<EventResource, Event>(saveEvent);

            Repository.Add(eventmap);

            await SaveChanges();
            await SaveHistoryAction("Add", saveEvent);

            return _mapper.Map<Event, EventResource>(eventmap); ;
        }

        public async Task<EventResource> Update(int id, ResourceEntity resourceEntity)
        {
            var eventSave = (EventResource)resourceEntity;
            var events = await Repository.GetOne<Event>().Include( e=> e.Schedules).FirstOrDefaultAsync(v => v.Id == id);

            if (events == null)
                throw new Exception("event not Found");

            //si le parametre delete est update pas besoin de vérifier que le titre existe déjà
            if (eventSave.IsDelete == events.IsDelete)
            {
                if (EventExist(eventSave.Title, eventSave.CeId))
                    throw new Exception("employee name :" + eventSave.Title + " is already taken");
            }

            _mapper.Map(eventSave, events);
            events.UpdatedOn = DateTime.Now;

            await SaveChanges();
            await SaveHistoryAction("Update", eventSave);

            var result = await GetItemById(events.Id);
            return result;
        }

        public async Task Delete(int id)
        {
            var events = await Repository.GetOne<Event>().FirstOrDefaultAsync(v => v.Id == id);

            if (events == null)
                throw new Exception("Event not Found");
            var resource = _mapper.Map<Event, EventResource>(events);

            Repository.Remove(events);
            await SaveChanges();
            await SaveHistoryAction("Delete", resource);
        }
    }
}
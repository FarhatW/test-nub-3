using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Mapping
{
    class ScheduleMappingProfile : Profile
    {
        public ScheduleMappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Schedule, ScheduleResource>().ForMember(sr => sr.InscriptionRessource,
                    opt => opt.MapFrom(s =>
                        s.EventSchedulesEmployees.Select(i => new InscriptionRessource()
                        {

                            CreatedBy = i.Employee.CreatedBy,
                            CreatedOn = i.Employee.CreatedOn,
                            UpdatedBy = i.Employee.UpdatedBy,
                            UpdatedOn = i.Employee.UpdatedOn,

                            NbChildren = i.NbChildren,
                            NbParticipantEvent = i.NbParticipantsEvent,
                            EmployeeFirstName = i.Employee.FirstName,
                            EmployeeLastName = i.Employee.LastName
                        })));

            CreateMap<Schedule, ScheduleSaveResource>().ForMember(s => s.Id, opt => opt.Ignore());

            //API Resource to Domaine
            CreateMap<ScheduleSaveResource, Schedule>()
                    .ForMember(s => s.Id, opt => opt.Ignore())
                    .ForMember(s => s.EventSchedulesEmployees, opt => opt.Ignore())
                    .AfterMap((sr, s) =>
                    {
                        var removedSchedulesEmployee = s.EventSchedulesEmployees.Where(ese => !sr.EventSchedulesEmployees.Any(se => se.ScheduleId == ese.ScheduleId)).ToList();
                        foreach (var item in removedSchedulesEmployee)
                        {
                            s.EventSchedulesEmployees.Remove(item);
                        }

                        var addedSchedulesEmployee = sr.EventSchedulesEmployees.Where(se => !s.EventSchedulesEmployees.Any(ese => ese.EmployeeId == se.EmployeeId)).Select(ese => new ScheduleEmployee { ScheduleId = ese.ScheduleId, EmployeeId = ese.EmployeeId, NbChildren = ese.NbChildren, NbParticipantsEvent = ese.NbParticipantsEvent }).ToList();
                        foreach (var item in addedSchedulesEmployee)
                        {
                            s.EventSchedulesEmployees.Add(item);
                        }
                    });
        }
    }
}

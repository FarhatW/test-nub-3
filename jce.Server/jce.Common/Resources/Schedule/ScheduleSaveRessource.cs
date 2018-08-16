using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.UserProfile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class ScheduleSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime ScheduleMin { get; set; }
        public DateTime ScheduleMax { get; set; }
        public int NbParticipant { get; set; }
        public bool IsActif { get; set; }
        public bool IsDelete { get; set; }
        public int EmployeeCeId { get; set; }
        public ICollection<ScheduleEmployee> EventSchedulesEmployees { get; set; }
        public ScheduleSaveResource()
        {
            EventSchedulesEmployees = new Collection<ScheduleEmployee>();
        }

    }
}

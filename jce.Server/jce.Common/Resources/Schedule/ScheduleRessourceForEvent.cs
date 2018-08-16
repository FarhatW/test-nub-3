using jce.Common.Core;
using jce.Common.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class ScheduleResourceForEvent : ResourceEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public DateTime ScheduleMin { get; set; }
        public DateTime ScheduleMax { get; set; }
        public int NbParticipant { get; set; }
        public ICollection<ScheduleEmployee> EventSchedulesEmployees { get; set; }
        public ScheduleResourceForEvent()
        {
            EventSchedulesEmployees = new Collection<ScheduleEmployee>();
        }

    }
}

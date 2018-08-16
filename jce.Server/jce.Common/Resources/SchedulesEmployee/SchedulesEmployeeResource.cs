using jce.Common.Core;
using jce.Common.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class SchedulesEmployeeResource : ResourceEntity
    {
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public int NbChildren { get; set; }
        public int NbParticipantsEvent { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }

        //public Schedule Schedules { get; set; }
        //public Entites.PersonJceProfile User { get; set; }
    }
}

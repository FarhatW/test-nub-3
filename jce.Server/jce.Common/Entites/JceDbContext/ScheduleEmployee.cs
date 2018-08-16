using System.ComponentModel.DataAnnotations.Schema;
using jce.Common.Entites.JceDbContext;

namespace jce.Common.Entites
{
    [Table("SchedulesEmployee")]
    public class ScheduleEmployee
    {
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public int NbParticipantsEvent { get; set; }
        public int NbChildren { get; set; }
        public bool IsDelete { get; set; }

        public virtual Schedule Schedules { get; set; }
        public virtual PersonJceProfile Employee { get; set; }
    }
}

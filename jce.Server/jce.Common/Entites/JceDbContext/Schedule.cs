using jce.Common.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jce.Common.Entites
{
    [Table("Schedules")]
    public class Schedule : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ScheduleMin { get; set; }

        [Required]
        public DateTime ScheduleMax { get; set; }

        public int NbParticipant { get; set; }

        [DefaultValue("true")]
        public bool IsActif { get; set; }

        public bool IsDelete { get; set; }

        [Required]
      
        public int EventId { get; set; }

        public ICollection<ScheduleEmployee> EventSchedulesEmployees { get; set; }

        public Schedule()
        {
            EventSchedulesEmployees = new Collection<ScheduleEmployee>();
        }


    }
}

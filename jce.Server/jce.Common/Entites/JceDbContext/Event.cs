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
    [Table("Events")]
    public class Event : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public int AdultNumber { get; set; }

        public bool IsDelete { get; set; }

        [DefaultValue("true")]
        public bool IsActif { get; set; }

        [Required]
        [ForeignKey("CEs")]
        public int CeId { get; set; }

        public ICollection<Schedule> Schedules { get; set; }

        public Event()
        {
            Schedules = new Collection<Schedule>();
        }

    }
}

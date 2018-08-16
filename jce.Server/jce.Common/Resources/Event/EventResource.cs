using jce.Common.Core;
using jce.Common.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class EventResource : ResourceEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int AdultNumber { get; set; }
        public bool IsActif { get; set; }
        public bool IsDelete { get; set; }
        public int CeId { get; set; }
        public ICollection<ScheduleResource> Schedules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Entites
{
    public class HistoryAction : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string ActionName { get; set; }

        [Required]
        [StringLength(255)]
        public string TableName { get; set; }


        [Required]
        public string UserId { get; set; }


        public string Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources
{
   public class HistoryActionResource :ResourceEntity
    {
        public int Id { get; set; }

        [Required]
        public string ActionName { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string UserId { get; set; }


        public string Content { get; set; }
    }
}

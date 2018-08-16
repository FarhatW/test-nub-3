using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Core.File
{
    public class DTODataNode
    {
        [Key]
        public int Id { get; set; }
        public string NodeName { get; set; }
        public int ParentId { get; set; }
    }
}

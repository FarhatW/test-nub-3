using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Core.File
{
    public class DTONode
    {
        [Key]
        public string data { get; set; }
        public string label { get; set; }
        public string expandedIcon { get; set; }
        public string collapsedIcon { get; set; }
        public List<DTONode> children { get; set; }
        public int parentId { get; set; }
        public string type { get; set; }
    }
}

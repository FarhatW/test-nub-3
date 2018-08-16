using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Entites.JceDbContext;

namespace jce.Common.Entites
{
    [Table("CommandChildProduct")]
    public class CommandChildProduct
    {
        public int CommandId { get; set; }
        public int ChildId { get; set; }
        public int ProductId { get; set; }
        public int OvertakeCommandChild { get; set; }

        public virtual Command Command { get; set; }
        public virtual Child Child { get; set; }
        public virtual Product Product { get; set; }

    }
}

using jce.Common.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jce.Common.Entites
{
    [Table("Command")]
    public class Command : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int OvertakeCommand { get; set; }

        public ICollection<CommandChildProduct> CommandChildProduct { get; set; }


        public Command()
        {
            CommandChildProduct = new Collection<CommandChildProduct>();
        }
    }
}

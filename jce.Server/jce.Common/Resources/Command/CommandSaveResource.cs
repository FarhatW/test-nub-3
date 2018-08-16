using jce.Common.Core;
using jce.Common.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class CommandSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public int OvertakeCommand { get; set; }
        public ICollection<CommandChildProduct> CommandChildProduct { get; set; }
        public CommandSaveResource()
        {
            CommandChildProduct = new Collection<CommandChildProduct>();
        }

    }
}

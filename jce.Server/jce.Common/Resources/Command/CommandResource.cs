using jce.Common.Core;
using jce.Common.Entites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class CommandResource : ResourceEntity
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public int OertakeCommand { get; set; }
        public ICollection<CommandChildProduct> CommandChildProduct { get; set; }
        public CommandResource()
        {
            CommandChildProduct = new Collection<CommandChildProduct>();
        }

    }
}

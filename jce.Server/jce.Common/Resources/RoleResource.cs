using System.ComponentModel.DataAnnotations;
using jce.Common.Core;

namespace jce.Common.Resources
{
    public class RoleResource : ResourceEntity
    {
        public int Id { get; set; }
                
        [Required]
        public string Name { get; set; }

        public bool Enable { get; set; }

        [Required]
        public int Rank { get; set; }
    }
}
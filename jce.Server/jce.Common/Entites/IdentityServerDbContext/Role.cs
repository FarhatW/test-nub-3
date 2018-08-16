using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using jce.Common.Core;
using Microsoft.AspNetCore.Identity;

namespace jce.Common.Entites.IdentityServerDbContext
{
    [Table("Roles")]
    public class Role : IdentityRole<int>
    {

        public Role()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;

        }
        public bool Enable { get; set; }
        public int Rank { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
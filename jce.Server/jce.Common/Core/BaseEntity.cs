using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jce.Common.Core
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseEntity()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

       
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

      
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace jce.Common.Core
{
    public abstract class ResourceEntity
    {
        protected ResourceEntity()
        {

            CreatedOn = DateTime.Now; 

            UpdatedOn = DateTime.Now; 
        }
       
        [Required]
        public string CreatedBy { get; set; }

       
        public string UpdatedBy { get; set; }


        public DateTime CreatedOn { get; set; }
        
    
        public DateTime UpdatedOn { get; set; }
    }
}

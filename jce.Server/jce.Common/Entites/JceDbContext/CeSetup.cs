using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml;
using jce.Common.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace jce.Common.Entites
{
    public class CeSetup : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Ces")]
        public int CeId { get; set; }
        public bool? IsHomeDelivery { get; set; }
        public bool? IsOrderConfirmationMail { get; set; }
        public bool? IsOrderConfirmationMail4CeAdmin { get; set; }
        public bool? IsAgeGroupChoiceLimitation { get; set; }
        public bool? IsCeParticipation { get; set; }
        public bool? IsEmployeeParticipation { get; set; }
        public bool? IsGroupingAllowed { get; set; }
        public string WelcomeMessage { get; set; }
        public bool? IsExceeding { get; set; }
        public bool? ChildCalculation { get; set; }
        public bool? CeCalculation { get; set; }
        //public bool? IsNoChildrenOrderAllowed { get; set; }

        public ICollection<Mail> Mail { get; set; }

        public CeSetup()
        {
            Mail = new Collection<Mail>();
        }


    }
}

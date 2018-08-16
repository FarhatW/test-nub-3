using System;
using System.Collections.Generic;
using System.Text;
using jce.Common.Core;

namespace jce.Common.Resources.CeSetup
{
    public class CeSetupResource : ResourceEntity
    {
        public int Id { get; set; }
        public int CeId { get; set; }
        public bool? IsHomeDelivery { get; set; }
        public bool? IsOrderConfirmationMail { get; set; }
        public bool? IsOrderConfirmationMail4CeAdmin { get; set; }
        public bool? IsAgeGroupChoiceLimitation { get; set; }
        public bool? IsCeParticipation { get; set; }
        public bool? IsEmployeeParticipation { get; set; }
        public bool? IsGroupingAllowed { get; set; }
        public string WelcomeMessage { get; set; }
        public bool? Isexceeding { get; set; }
        public bool? ChildCalculation { get; set; }
        public bool? CeCalculation { get; set; }
    }
}

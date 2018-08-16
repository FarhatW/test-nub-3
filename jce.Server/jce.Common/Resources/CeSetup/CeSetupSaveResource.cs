using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;

namespace jce.Common.Resources.CeSetup
{
    public class CeSetupSaveResource : ResourceEntity
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
        public ICollection<Mail> MailCe { get; set; }

        public CeSetupSaveResource()
        {
            MailCe = new Collection<Mail>();
        }

        //public bool? IsNoChildrenOrderAllowed { get; set; }

    }
}

using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Resources.UserProfile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class MailResource : ResourceEntity
    {
        public int Id { get; set; }
        public string MailObject { get; set; }
        public string MailBody { get; set; }
        public string MailType { get; set; }
     
        //public ICollection<InscriptionRessource> InscriptionRessource { get; set; }
        //public ScheduleResource()
        //{
        //    InscriptionRessource = new Collection<InscriptionRessource>();
        //}

    }
}

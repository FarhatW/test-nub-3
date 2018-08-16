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
    public class InscriptionRessource : ResourceEntity
    {
        public int NbChildren { get; set; }
        public int NbParticipantEvent { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public bool IsDelete { get; set; }
    }
}

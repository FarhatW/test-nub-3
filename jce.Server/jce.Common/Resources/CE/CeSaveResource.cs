﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites;
using jce.Common.Entites.JceDbContext;
using jce.Common.Resources.CeSetup;

namespace jce.Common.Resources.CE
{
    public class CeSaveResource : ResourceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool? Actif { get; set; }
        public bool? IsDeleted { get; set; }
        //address & contact
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public CeSetupResource CeSetup { get; set; }

        public int AdminJceProfileId { get; set; }

        public AddressResource Address { get; set; }
        public ICollection<PersonJceProfile> UserProfiles { get; set; }
        public CatalogResource Catalog { get; set; }

        public CeSaveResource()
        {
            UserProfiles = new Collection<PersonJceProfile>();
        }

    }
}

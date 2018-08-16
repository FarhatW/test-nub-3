using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources.Good
{
    public class GoodToCatalogSaveResource
    {
        public int GoodId { get; set; }
        public int CatalogId { get; set; }
        public string ClientProductAlias { get; set; }
        public string EmployeeParticipationMessage { get; set; }

        [Required]
        public int DateMin { get; set; }

        [Required]
        public int DateMax { get; set; }

        public Boolean? IsAddedManually { get; set; }
    }
}

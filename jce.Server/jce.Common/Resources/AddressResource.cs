using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace jce.Common.Resources
{
    public class AddressResource
    {
        public string Agency { get; set; }

        public string Service { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        public string AddressExtra { get; set; }
    }
}

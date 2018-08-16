using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace jce.Common.Entites.JceDbContext
{
    [Table("AdminJceProfiles")]
     public class AdminJceProfile : JceProfile
    {
        public ICollection<Ce> Ces { get; set; }


        public AdminJceProfile(int id, string firstName, string lastName, string email, string agency, string service, 
            string company, string streetNumber, string address1, string address2, string postalCode, string phone, 
            string city, string addressExtra, bool isEnabled, ICollection<Ce> ces) : base(id, firstName, lastName, email, 
                agency, service, company, streetNumber, address1, address2, postalCode, phone, city, addressExtra, isEnabled)
        {
            Ces = ces;
        }

        public AdminJceProfile()
        {
            Ces = new List<Ce>();
        }
    }
}

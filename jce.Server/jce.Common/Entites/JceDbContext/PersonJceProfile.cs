using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using jce.Common.Core;

namespace jce.Common.Entites.JceDbContext
{
    [Table("PersonJceProfiles")]
    public class PersonJceProfile : JceProfile
    {
        [ForeignKey("Ces")]
        public int CeId { get; set; }
        public ICollection<Child> Children { get; set; }

       

        public PersonJceProfile(int id, string firstName, string lastName, string email, string agency, string service, 
            string company, string streetNumber, string address1, string address2, string postalCode, string phone, string city, 
            string addressExtra, bool isEnabled, int ceId, ICollection<Child> children) : base(id, firstName, lastName, email, agency, 
                service, company, streetNumber, address1, address2, postalCode, phone, city, addressExtra, isEnabled)
        {
            CeId = ceId;
            Children = children;
        }
        public PersonJceProfile()
        {
            Children = new List<Child>();
        }
    }
}
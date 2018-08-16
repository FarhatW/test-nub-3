using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using jce.Common.Core;
using jce.Common.Entites.IdentityServerDbContext;

namespace jce.Common.Entites.JceDbContext
{
    [Table("JceProfiles")]
    public abstract class JceProfile : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]

        [StringLength(255)]
        public string Email { get; set; }
        //Adress
        [Required]
        [StringLength(255)]
        public string Agency { get; set; }
        [Required]
        [StringLength(255)]
        public string Service { get; set; }
        [Required]
        [StringLength(255)]
        public string Company { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        [StringLength(255)]
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }
        public string AddressExtra { get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        protected JceProfile(int id, string firstName, string lastName, string email, string agency, string service, string company, string streetNumber, string address1, string address2, string postalCode, string phone, string city, string addressExtra, bool isEnabled)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Agency = agency;
            Service = service;
            Company = company;
            StreetNumber = streetNumber;
            Address1 = address1;
            Address2 = address2;
            PostalCode = postalCode;
            Phone = phone;
            City = city;
            AddressExtra = addressExtra;
            IsEnabled = isEnabled;
        }

        protected JceProfile()
        {
           
        }
    }
}

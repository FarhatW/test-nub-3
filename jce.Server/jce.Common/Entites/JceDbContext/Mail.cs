using jce.Common.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jce.Common.Entites
{
    [Table("Mail")]
    public class Mail : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MailObject { get; set; }

        [Required]
        public string MailBody { get; set; }

        [Required]
        public string MailType { get; set; }

        public int CeSetupId { get; set; }
    }
}

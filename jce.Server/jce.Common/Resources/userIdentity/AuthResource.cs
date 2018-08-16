using System.ComponentModel.DataAnnotations;

namespace jce.Common.Resources.UserProfile
{
    public class AuthResource
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}

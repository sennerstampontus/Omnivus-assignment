using System.ComponentModel.DataAnnotations;

namespace OmnivusMvcWebsite.Models
{
    public class SignInForm
    {
        public SignInForm()
        {
           
        }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Must provide an email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "The email must be valid")]
        public string Email { get; set; } = "";

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Must provide a password")]
        public string Password { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public string ReturnUrl { get; set; } = "/";
    }
}

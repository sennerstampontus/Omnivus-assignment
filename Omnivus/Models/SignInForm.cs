using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class SignInForm
    {
        public SignInForm()
        {
            Email = "";
            Password = "";
            ReturnUrl = "/";
        }


        [Display(Name = "Epostadress")]
        [Required(ErrorMessage = "Epostadress kan inte vara tomt")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Du måste ange en giltig e-postadress")]
        public string Email { get; set; }

        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace OmnivusMvcWebsite.Models
{
    public class SignUpForm
    {

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Must provide first name")]
        [StringLength(256, ErrorMessage = "First name must be at least 2 characters", MinimumLength = 2)]
        public string FirstName { get; set; } = "";

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Must provide last name")]
        [StringLength(256, ErrorMessage = "Last name must be at least 2 characters", MinimumLength = 2)]
        public string LastName { get; set; } = "";

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Must provide a valid email")]
        public string Email { get; set; } = "";

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Must provide a password")]
        public string Password { get; set; } = "";

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Must confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; } = "";


        [Display(Name = "Street name")]
        [Required(ErrorMessage = "Must provide street name")]
        [StringLength(256, ErrorMessage = "Street name must be at least 2 characters", MinimumLength = 2)]
        public string StreetName { get; set; } = "";

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "Must provide postal code")]
        [StringLength(256, ErrorMessage = "Postal code must be at least 2 characters", MinimumLength = 5)]
        public string PostalCode { get; set; } = "";

        [Display(Name = "City")]
        [Required(ErrorMessage = "Must provide city")]
        [StringLength(256, ErrorMessage = "City must be at least 2 characters", MinimumLength = 2)]
        public string City { get; set; } = "";

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Must provide country")]
        [StringLength(256, ErrorMessage = "Country must be at least 2 characters", MinimumLength = 2)]
        public string Country { get; set; } = "";

        public string ErrorMessage { get; set; } = "";
        public string ReturnUrl { get; set; } = "/";

        public string RoleName { get; set; } = "User";

    }
}

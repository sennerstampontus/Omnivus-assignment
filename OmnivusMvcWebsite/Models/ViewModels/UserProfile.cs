namespace OmnivusMvcWebsite.Models.ViewModels
{
    public class UserProfile
    {
        public UserProfile()
        {

        }

        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = "Sweden";

        public string FileName { get; set; }
        public string FriendlyFileName { get; set; }
        public IFormFile File { get; set; }
    }
}

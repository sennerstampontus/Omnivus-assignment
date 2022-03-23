using Microsoft.AspNetCore.Identity;

namespace OmnivusMvcWebsite.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public UserEntityModel UserEntity { get; set; }
    }
}

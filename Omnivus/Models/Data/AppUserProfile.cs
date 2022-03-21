using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omnivus.Models.Data
{
    internal class AppUserProfile
    {
        public AppUserProfile()
        {

        }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; } = "";
        public IdentityUser User { get; set; } = new();

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string ProfileId { get; set; } = "";
        public AppProfile Profile { get; set; } = new();
    }
}


using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmnivusMvcWebsite.Models
{
    public class ProfileEntity
    {
        [Key]
        [Column(TypeName = "nvarchar(450)")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = string.Empty;


        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Bio { get; set; } = string.Empty;


        [Column(TypeName = "nvarchar(50)")]
        public string StreetName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(6)")]
        public string PostalCode { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; } = "Sweden";

        public string UserId { get; set; } = string.Empty;
        public virtual IdentityUser User { get; set; }

    }
}

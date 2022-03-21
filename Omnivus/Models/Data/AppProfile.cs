using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Omnivus.Models.Data
{
    public class AppProfile
    {
        [Key]
        [Column(TypeName = "nvarchar(450)")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Bio { get; set; } = "";

        [Required]
        [PersonalData]
        public int AddressId { get; set; }
        public virtual AppAddress Address { get; set; }
    }
}


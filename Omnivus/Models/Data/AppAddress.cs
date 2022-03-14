using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models.Data
{
    public class AppAddress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [PersonalData]
        public string StreetName { get; set; }
        [Required]
        [PersonalData]
        public string PostalCode { get; set; }
        [Required]
        [PersonalData]
        public string City { get; set; }
    }
}

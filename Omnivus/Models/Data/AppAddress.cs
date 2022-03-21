using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models.Data
{
    public class AppAddress
    {

        public AppAddress()
        {

        }

        public AppAddress(string streetName, string postalCode, string city)
        {
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
        }

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

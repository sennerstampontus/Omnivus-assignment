﻿namespace OmnivusMvcWebsite.Models.ViewModels
{
    public class UserProfile
    {


        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = "Sweden";
    }
}

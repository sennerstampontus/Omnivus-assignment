using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models.Data
{

    public class AppUserAddress
    {
        public AppUserAddress()
        {

        }

      public string UserId { get; set; }
      public int AddressId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual AppAddress Address { get; set; }
    }
}

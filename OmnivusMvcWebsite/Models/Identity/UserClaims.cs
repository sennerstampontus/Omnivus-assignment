using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace OmnivusMvcWebsite.Models.Identity
{
    public class UserClaims : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        public UserClaims(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            
            var claimsIdentity = await base.GenerateClaimsAsync(user);

            var roles = await base.UserManager.GetRolesAsync(user);
            string role = "";
            foreach (var _role in roles)
            {
                role = $"{_role} ";
            }

            claimsIdentity.AddClaim(new Claim("UserId", user.Id));
            claimsIdentity.AddClaim(new Claim("UserRole", role));
            

            return claimsIdentity;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OmnivusMvcWebsite.Data;
using OmnivusMvcWebsite.Models;
using OmnivusMvcWebsite.Models.ViewModels;

namespace OmnivusMvcWebsite.Services
{
    public interface IProfileManager
    {
        Task<ProfileResult> CreateAsync(IdentityUser user, UserProfile profile);
        Task<UserProfile> ReadAsync(string userId);
        Task<AdminViewModel> ReadAllAsync(string userId);
        Task<string> ReadRoleAsync(string userId);
        Task<string> DisplayNameAsync(string userId);
    }

   
    public class ProfileManager : IProfileManager
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileManager(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<ProfileResult> CreateAsync(IdentityUser user, UserProfile profile)
        {
            if(await _context.Users.AnyAsync(x => x.Id == user.Id))
            {
                var profileEntity = new ProfileEntity
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Email = profile.Email,
                    StreetName = profile.StreetName,
                    PostalCode = profile.PostalCode,
                    City = profile.City,
                    Country = profile.Country,
                    UserId = user.Id
                };

                _context.Profiles.Add(profileEntity);
                await _context.SaveChangesAsync();

                return new ProfileResult { Succeeded = true };
            }

            return new ProfileResult { Succeeded = false };
        }

        public async Task<string> DisplayNameAsync(string userId)
        {
            var res = await ReadAsync(userId);
            return $"{res.FirstName} {res.LastName}";
        }

        public async Task<UserProfile> ReadAsync(string userId)
        {
            var profile = new UserProfile();
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            if(profileEntity != null)
            {
                profile.FirstName = profileEntity.FirstName;
                profile.LastName = profileEntity.LastName;
                profile.Email = profileEntity.Email;
                profile.StreetName = profileEntity.StreetName;
                profile.PostalCode = profileEntity.PostalCode;
                profile.City = profileEntity.City;
                profile.Country = profileEntity.Country;
                profile.Bio = profileEntity.Bio;
                profile.FileName = profileEntity.FileName;
            }
            return profile;
        }

        public async Task<AdminViewModel> ReadAllAsync(string userId)
        {
            var profiles = new List<UserProfile>();
            var profile = new UserProfile();
            var adminProfile = new AdminViewModel();

            var profilesEntity = await _context.Profiles.Include(x => x.User).ToListAsync();
            var profileEntity = await _context.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);

            if (profilesEntity != null)
            {
                foreach (var _profile in profilesEntity)
                {
                    profiles.Add(new UserProfile()
                    {
                        FirstName = _profile.FirstName,
                        LastName = _profile.LastName,
                        Email = _profile.Email,
                        StreetName = _profile.StreetName,
                        PostalCode = _profile.PostalCode,
                        City = _profile.City,
                        Country = _profile.Country,
                        Bio = _profile.Bio,
                        FileName = _profile.FileName
                });
                }
            }

            if(profileEntity != null)
            {
                profile.FirstName = profileEntity.FirstName;
                profile.LastName = profileEntity.LastName;
            }

            adminProfile.UserProfile = profile;
            adminProfile.UserProfiles = profiles;
            

            return adminProfile;
        }

        public async Task<string> ReadRoleAsync(string userId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);

            var role = await _roleManager.FindByIdAsync(userRole.RoleId);

            return role.Name;
        }
    }

    public class ProfileResult
    {
        public bool Succeeded { get; set; } = false;
    }
}

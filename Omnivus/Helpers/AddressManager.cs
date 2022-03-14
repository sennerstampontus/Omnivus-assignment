using Microsoft.EntityFrameworkCore;
using Omnivus.Models.Data;

namespace Omnivus.Helpers
{
    public interface IAddressManager
    {
        Task<IEnumerable<AppAddress>> GetAddressesAsync();
        Task<AppAddress> GetUserAddressAsync(AppUser user);
        Task CreateUserAddressAsync(AppUser user, AppAddress address);
    }

    public class AddressManager : IAddressManager
    {
        private readonly AppDbContext _context;

        public AddressManager(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAddressAsync(AppUser user, AppAddress address)
        {
            var userAddress = new AppUserAddress();
            var _address = _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == address.StreetName && x.PostalCode == address.PostalCode);
            if(_address == null)
            {
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                userAddress = new AppUserAddress { UserId = user.Id, AddressId = address.Id };
            }
            else
            {
                userAddress = new AppUserAddress { UserId = user.Id, AddressId = _address.Id };
            }


            _context.UserAddresses.Add(userAddress);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppAddress>> GetAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<AppAddress> GetUserAddressAsync(AppUser user)
        {
            var _user = await _context.UserAddresses.Include(x => x.Address).FirstOrDefaultAsync(x => x.UserId == user.Id);

            return _user.Address;
        }
    }
}

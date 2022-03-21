using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Omnivus.Models.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       
        public DbSet<AppAddress> Addresses { get; set; }
        public DbSet<AppUserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserAddress>()
                .HasKey(x => new {x.UserId, x.AddressId});

            modelBuilder.Entity<AppUserProfile>()
                .HasKey(x => new { x.UserId, x.ProfileId});

            
            modelBuilder.Entity<AppProfile>()
                .HasOne(x => x.Address)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

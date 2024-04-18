using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookingApp.Models.Booking;


namespace OnlineBookingApp.Data
{
    public class OnlineBookingDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineBookingDbContext(DbContextOptions<OnlineBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel>? hotels { get; set; }
        public DbSet<CarRental>? cars { get; set; }
        public DbSet<Flight>? flights { get; set; }
        public DbSet<Guest>? guests { get; set; }
        public DbSet<GuestBooking>? guestBooking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base class method

            // Define customizations here if needed
        }
    }
}

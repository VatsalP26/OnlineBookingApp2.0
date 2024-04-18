using System.ComponentModel.DataAnnotations;

namespace OnlineBookingApp.Models.Booking
{
    public class CarRental
    {
        public int CarRentalId { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public string RentalCompany { get; set; }

        [Required]
        public int Price { get; set; }

        // Additional properties like availability can be added here
    }
}

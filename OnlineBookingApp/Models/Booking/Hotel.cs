
using System.ComponentModel.DataAnnotations;

namespace OnlineBookingApp.Models.Booking
{
    public class Hotel
    {
        public int HotelId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int Price { get; set; }

        // Additional properties like amenities can be added here

    }
}

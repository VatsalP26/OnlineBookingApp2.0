using System.ComponentModel.DataAnnotations;

namespace OnlineBookingApp.Models.Booking
{
    public class BookingGuest
    {
       
            public int BookingId { get; set; }

            [Required]
            public int FlightId { get; set; }
            public Flight Flight { get; set; }

            [Required]
            public int HotelId { get; set; }
            public Hotel Hotel { get; set; }

            [Required]
            public int CarRentalId { get; set; }
            public CarRental CarRental { get; set; }

            // Additional properties like booking date, guest details can be added here
        
    }
}

using System.ComponentModel.DataAnnotations;

namespace OnlineBookingApp.Models.Booking
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required]
        public string Airline { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int Price { get; set; }
    }

}


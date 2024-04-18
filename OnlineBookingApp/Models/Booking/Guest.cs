
   using System.ComponentModel.DataAnnotations;

   namespace OnlineBookingApp.Models.Booking
{
        public class Guest
        {
            public Guid Id { get; set; }

            [Required]
            public string? Name { get; set; }

            [Required]
            [EmailAddress]
            public string? Email { get; set; }

           
        }
    }



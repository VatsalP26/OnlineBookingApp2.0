using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookingApp.Models.Booking;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookingApp.Data;
using OnlineBookingApp.Models;

namespace YourNamespace.Controllers
{
    public class SerchController : Controller
    {
        private readonly OnlineBookingDbContext _context;

        public SerchController(OnlineBookingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string airline, DateTime? arrivalTime)
        {
            // Redirect to FlightController's Index action with search parameters
            return RedirectToAction("Index", "Flight", new { airline, arrivalTime });
        }

        [HttpGet]
        public IActionResult HotelIndex()
        {
            return View();
        }


        [HttpPost]
        public IActionResult HotelIndex(string searchString)
        {
            // Redirect to FlightController's Index action with search parameters
            return RedirectToAction("Index", "Hotel", new { searchString });
        }

        public IActionResult CarIndex()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CarIndex(string searchString)
        {
            // Redirect to FlightController's Index action with search parameters
            return RedirectToAction("Index", "CarRental", new { searchString });
        }
    }
}

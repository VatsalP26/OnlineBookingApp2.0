using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookingApp.Models.Booking;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookingApp.Data;
using OnlineBookingApp.Models;

namespace YourNamespace.Controllers
{
    public class GuestController : Controller
    {
        private readonly OnlineBookingDbContext _context;

        public GuestController(OnlineBookingDbContext context)
        {
            _context = context;
        }

        // GET: Guest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guest/Create
        //
        public async Task<IActionResult> Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(guest);
        }


    }
}

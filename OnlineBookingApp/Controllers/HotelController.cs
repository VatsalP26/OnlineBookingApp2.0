using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookingApp.Data;
using OnlineBookingApp.Models.Booking;
using System.Linq;
using System.Threading.Tasks;

public class HotelController : Controller
{
    private readonly OnlineBookingDbContext _context;

    public HotelController(OnlineBookingDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        // Retrieve all hotels
        IQueryable<Hotel> hotels = _context.hotels;

        // Apply search filter if search string is provided
        if (!string.IsNullOrEmpty(searchString))
        {
            hotels = hotels.Where(h => h.Name.Contains(searchString));
        }

        return View(await hotels.ToListAsync());
    }

    // GET: Hotel/Details/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hotel = await _context.hotels.FirstOrDefaultAsync(m => m.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return View(hotel);
    }

    // GET: Hotel/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Hotel/Create
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Flight flight)
    {
        if (ModelState.IsValid)
        {
            // Add flight to database
            _context.flights.Add(flight);
            await _context.SaveChangesAsync();

            // Set confirmation message
            TempData["BookingConfirmation"] = "Booking confirmed successfully.";

            // Redirect back to Create action
           // return RedirectToAction(nameof(Create));
        }

        return View();
    }

    [Authorize(Roles = "Admin")]
    // GET: Hotel/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hotel = await _context.hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return View(hotel);
    }
    [Authorize(Roles = "Admin")]
    // POST: Hotel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("HotelId,Name,Location,Price,Rating")] Hotel hotel)
    {
        //if (id != hotel.HotelId)
        //{
        //    return NotFound();
        //}

        Hotel existingHotel = await _context.hotels.FirstOrDefaultAsync(x => x.HotelId == id);
        if (existingHotel == null)
        {
            return NotFound();
        }

        existingHotel.Name = hotel.Name;
        existingHotel.Location = hotel.Location;
        existingHotel.Price = hotel.Price;
       // existingHotel.Rating = hotel.Rating;

        try
        {
            _context.Update(existingHotel);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HotelExists(hotel.HotelId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    // GET: Hotel/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hotel = await _context.hotels.FirstOrDefaultAsync(m => m.HotelId == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return View(hotel);
    }
    [Authorize(Roles = "Admin")]
    // POST: Hotel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var hotel = await _context.hotels.FindAsync(id);
        _context.hotels.Remove(hotel);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> Book(int hotelId)
    {
        var hotel = await _context.hotels.FindAsync(hotelId);
        if (hotel == null)
        {
            return NotFound();
        }

        var booking = new GuestBooking
        {
          //  HotelId = hotelId,
            // Add other properties as needed
        };

        _context.guestBooking.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToAction("BookingConfirmation");
    }

    public IActionResult BookingConfirmation()
    {
        return View();
    }
    private bool HotelExists(int id)
    {
        return _context.hotels.Any(e => e.HotelId == id);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookingApp.Data;
using OnlineBookingApp.Models.Booking;
using System.Linq;
using System.Threading.Tasks;

public class CarRentalController : Controller
{
    private readonly OnlineBookingDbContext _context;

    public CarRentalController(OnlineBookingDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string searchTerm)
    {
        // Retrieve all car rentals
        IQueryable<CarRental> carRentals = _context.cars;

        // Apply search filter if provided
        if (!string.IsNullOrEmpty(searchTerm))
        {
            carRentals = carRentals.Where(c => c.CarModel.Contains(searchTerm)
                                            || c.RentalCompany.Contains(searchTerm));
        }

        // Store search term in ViewBag to maintain state in the view
        ViewBag.SearchTerm = searchTerm;

        // Execute query and pass filtered car rentals to the view
        return View(await carRentals.ToListAsync());
    }


    // GET: CarRental/Details/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carRental = await _context.cars.FirstOrDefaultAsync(m => m.CarRentalId == id);
        if (carRental == null)
        {
            return NotFound();
        }

        return View(carRental);
    }

    // GET: CarRental/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: CarRental/Create
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CarRentalId,CarModel,RentalCompany,Price")] CarRental carRental)
    {
        if (ModelState.IsValid)
        {
            _context.Add(carRental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(carRental);
    }

    // GET: CarRental/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carRental = await _context.cars.FindAsync(id);
        if (carRental == null)
        {
            return NotFound();
        }
        return View(carRental);
    }

    // POST: CarRental/Edit/5
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CarRentalId,CarModel,RentalCompany,Price")] CarRental carRental)
    {


        CarRental car = await _context.cars.FirstOrDefaultAsync(x => x.CarRentalId == id);

        if(car == null)
        {
            return NotFound();
        }

       else
        {
            car.CarModel = carRental.CarModel;
            car.RentalCompany = carRental.RentalCompany;
            car.Price = carRental.Price;
            try
            {
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarRentalExists(carRental.CarRentalId))
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
       // return View(carRental);
    }

    // GET: CarRental/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carRental = await _context.cars.FirstOrDefaultAsync(m => m.CarRentalId == id);
        if (carRental == null)
        {
            return NotFound();
        }

        return View(carRental);
    }
    [Authorize(Roles = "Admin")]
    // POST: CarRental/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var carRental = await _context.cars.FindAsync(id);
        _context.cars.Remove(carRental);
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
           
        };

        _context.guestBooking.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToAction("BookingConfirmation");
    }

    public IActionResult BookingConfirmation()
    {
        return View();
    }
    private bool CarRentalExists(int id)
    {
        return _context.cars.Any(e => e.CarRentalId == id);
    }
}

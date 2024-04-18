using Microsoft.AspNetCore.Mvc;

namespace OnlineBookingApp.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookingApp.Data;
using System.Threading.Tasks;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

   
    public IActionResult AssignRole()
    {
        // Retrieve all users
        var users = _userManager.Users;
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                // Create the role if it doesn't exist
                var newRole = new IdentityRole(roleName);
                await _roleManager.CreateAsync(newRole);
            }

            // Assign the role to the user
            await _userManager.AddToRoleAsync(user, roleName);
            return RedirectToAction("UserDetails", new { userId = userId });
        }
        return RedirectToAction("UserNotFound");
    }

    public async Task<IActionResult> UserDetails(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            ViewData["UserManager"] = _userManager;
            return View(user);
        }
        return RedirectToAction("UserNotFound");
    }

    public IActionResult UserNotFound()
    {
        return View();
    }
}

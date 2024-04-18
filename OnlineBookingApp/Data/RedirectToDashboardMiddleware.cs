using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using OnlineBookingApp.Data;

public class RedirectToDashboardMiddleware
{
    private readonly RequestDelegate _next;

    public RedirectToDashboardMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager, LinkGenerator linkGenerator)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            // If not authenticated, redirect to login page
            context.Response.Redirect("/Account/Login");
            return;
        }

        var user = await userManager.GetUserAsync(context.User);
        if (user == null)
        {
            // If user not found, redirect to login page
            context.Response.Redirect("/Account/Login");
            return;
        }

        if (await userManager.IsInRoleAsync(user, "Admin"))
        {
            // If user is admin, redirect to admin dashboard
            context.Response.Redirect(linkGenerator.GetPathByAction("AssignRole", "Admin"));
            return;
        }

        // If not admin, redirect to user dashboard or home page
        context.Response.Redirect(linkGenerator.GetPathByAction("Index", "Home"));
    }
}

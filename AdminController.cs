using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Data;

namespace LocalServiceBooking.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    public AdminController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login", "Account");

        ViewBag.Total = await _context.Bookings.CountAsync();
        ViewBag.Pending = await _context.Bookings.CountAsync(b => b.Status == "Pending");
        ViewBag.Completed = await _context.Bookings.CountAsync(b => b.Status == "Completed");
        ViewBag.Providers = await _context.ServiceProviders.CountAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login", "Account");

        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            booking.Status = status;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Bookings");
    }

    public async Task<IActionResult> Bookings()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login", "Account");
        return View(await _context.Bookings.OrderByDescending(b => b.Id).ToListAsync());
    }
}

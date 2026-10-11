using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Data;

namespace LocalServiceBooking.Controllers;

public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;
    public CustomerController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Bookings(string? mobile)
    {
        if (string.IsNullOrWhiteSpace(mobile))
            return View();

        var bookings = await _context.Bookings
            .Where(b => b.Mobile == mobile)
            .OrderByDescending(b => b.Id)
            .ToListAsync();

        var ids = await _context.Reviews
            .Where(r => bookings.Select(b => b.Id).Contains(r.BookingId))
            .Select(r => r.BookingId)
            .ToListAsync();

        ViewBag.Mobile = mobile;
        ViewBag.ReviewedBookingIds = ids;
        return View(bookings);
    }
}

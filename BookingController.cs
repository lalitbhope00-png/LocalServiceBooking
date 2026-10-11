using Microsoft.AspNetCore.Mvc;
using LocalServiceBooking.Data;
using LocalServiceBooking.Models;

namespace LocalServiceBooking.Controllers;

public class BookingController : Controller
{
    private readonly ApplicationDbContext _context;
    public BookingController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Index(string? service)
    {
        ViewBag.Service = service;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(Booking booking)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Service = booking.Service;
            return View(booking);
        }

        if (string.IsNullOrWhiteSpace(booking.Status))
            booking.Status = "Pending";

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return View("Success", booking);
    }
}

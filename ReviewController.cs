using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Data;
using LocalServiceBooking.Models;

namespace LocalServiceBooking.Controllers;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    public ReviewController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Create(int bookingId)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);
        if (booking == null) return NotFound();
        if (booking.Status != "Completed") return BadRequest("Review can be submitted only for completed bookings.");
        if (await _context.Reviews.AnyAsync(r => r.BookingId == bookingId))
            return BadRequest("Review has already been submitted for this booking.");

        ViewBag.BookingId = bookingId;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Review review)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == review.BookingId);
        if (booking == null) return NotFound();

        if (booking.Status != "Completed")
            ModelState.AddModelError("", "Review can be submitted only for completed bookings.");

        if (await _context.Reviews.AnyAsync(r => r.BookingId == review.BookingId))
            ModelState.AddModelError("", "Review has already been submitted for this booking.");

        if (booking.Mobile != review.Mobile)
            ModelState.AddModelError("Mobile", "Mobile number does not match the booking.");

        if (booking.CustomerName != review.CustomerName)
            ModelState.AddModelError("CustomerName", "Customer name does not match the booking.");

        if (!ModelState.IsValid)
        {
            ViewBag.BookingId = review.BookingId;
            return View(review);
        }

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Bookings", "Customer", new { mobile = review.Mobile });
    }
}

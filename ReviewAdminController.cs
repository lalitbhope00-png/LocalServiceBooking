using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Data;

namespace LocalServiceBooking.Controllers;

public class ReviewAdminController : Controller
{
    private readonly ApplicationDbContext _context;
    public ReviewAdminController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetString("IsAdmin") != "true")
            return RedirectToAction("Login", "Account");

        return View(await _context.Reviews.OrderByDescending(r => r.Id).ToListAsync());
    }
}

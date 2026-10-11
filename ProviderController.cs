using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalServiceBooking.Data;
using LocalServiceBooking.Models;

namespace LocalServiceBooking.Controllers;

public class ProviderController : Controller
{
    private readonly ApplicationDbContext _context;
    public ProviderController(ApplicationDbContext context) => _context = context;

    private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "true";

    public async Task<IActionResult> Index()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        return View(await _context.ServiceProviders.OrderBy(p => p.ProviderName).ToListAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceProvider provider)
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        if (!ModelState.IsValid) return View(provider);
        _context.ServiceProviders.Add(provider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        var p = await _context.ServiceProviders.FindAsync(id);
        return p == null ? NotFound() : View(p);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ServiceProvider provider)
    {
        if (!IsAdmin()) return RedirectToAction("Login", "Account");
        if (!ModelState.IsValid) return View(provider);
        _context.ServiceProviders.Update(provider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

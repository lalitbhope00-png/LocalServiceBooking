using Microsoft.AspNetCore.Mvc;

namespace LocalServiceBooking.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login() => View("Index");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "admin123")
        {
            HttpContext.Session.SetString("IsAdmin", "true");
            return RedirectToAction("Index", "Admin");
        }

        ViewBag.Error = "Invalid username or password";
        return View("Index");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

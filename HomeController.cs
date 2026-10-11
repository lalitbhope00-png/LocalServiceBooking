using Microsoft.AspNetCore.Mvc;

namespace LocalServiceBooking.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}

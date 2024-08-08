using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    [Authorize(Roles = "Admin")]

    public IActionResult Index()
    {
        return View();
    }
}

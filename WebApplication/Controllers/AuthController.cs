using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}

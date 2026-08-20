using Microsoft.AspNetCore.Mvc;

namespace MVCApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

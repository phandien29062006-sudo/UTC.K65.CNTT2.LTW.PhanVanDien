using Microsoft.AspNetCore.Mvc;

namespace NetCoreLA7.Areas.Admins.Controllers
{
    [Area("Admins")]  
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

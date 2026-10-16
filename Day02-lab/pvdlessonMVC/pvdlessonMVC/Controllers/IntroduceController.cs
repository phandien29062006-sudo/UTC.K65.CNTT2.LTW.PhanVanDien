using Microsoft.AspNetCore.Mvc;

namespace pvdlessonMVC.Controllers
{
    public class IntroduceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

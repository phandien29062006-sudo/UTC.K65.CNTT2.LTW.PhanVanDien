using Microsoft.AspNetCore.Mvc;

namespace pvdlessonMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

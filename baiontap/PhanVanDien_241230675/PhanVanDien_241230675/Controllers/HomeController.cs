using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhanVanDien_241230675.Models;
using System.Diagnostics;

namespace PhanVanDien_241230675.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLNhaSachContext _context;
        public HomeController (QLNhaSachContext context)
        {
            _context = context;
        }
        public IActionResult Index()

        {
            ViewBag.TheLoai = _context.TheLoais.ToList();
            ViewBag.Sach = _context.Saches.Where(s => s.Gia.HasValue && s.Gia.Value >= 100).ToList();
            ViewBag.TieuDe = "PHAN VAN DIEN";
            return View();
        }
        public IActionResult BooksByCategory(int maTL)
        {
            var theLoai = _context.TheLoais.FirstOrDefault(t => t.MaTl == maTL);
            var sach = _context.Saches.Where(s => s.MaTl == maTL).ToList();
            ViewBag.TieuDe = theLoai.TenTl;
            return PartialView("PhanVanDien_MainContent", sach);
        }
        public IActionResult Create()
        {
            ViewBag.TheLoai = _context.TheLoais.ToList();
            ViewBag.MaTL = new SelectList(
                _context.TheLoais.ToList(),
                "MaTl", "TenTl"
            );
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sach sach)
        {
            _context.Saches.Add(sach);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

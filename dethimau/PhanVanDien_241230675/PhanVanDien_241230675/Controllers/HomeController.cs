using Microsoft.AspNetCore.Mvc;
using PhanVanDien_241230675.Models;
using System.Diagnostics;

namespace PhanVanDien_241230675.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLHangHoaContext _context;
        public HomeController( QLHangHoaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.LoaiHang = _context.LoaiHangs.ToList();
            ViewBag.HangHoa = _context.HangHoas.Where(s => s.Gia.HasValue && s.Gia.Value >= 100).ToList();
            ViewBag.TieuDe = "Phan Văn Diễn";
            return View();
        }
        public IActionResult LocHangHoa(int maLH)
        {
            var loaiHang = _context.LoaiHangs.FirstOrDefault(h => h.MaLoai == maLH);
            var hangHoa = _context.HangHoas.Where(h => h.MaLoai == maLH).ToList();
            ViewBag.TieuDe = loaiHang.TenLoai;
            return PartialView("PhanVanDien_MainContent", hangHoa);
        }
        public IActionResult Create()
        {
            ViewBag.LoaiHang = _context.LoaiHangs.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangHoa h)
        {
            _context.HangHoas.Add(h);
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

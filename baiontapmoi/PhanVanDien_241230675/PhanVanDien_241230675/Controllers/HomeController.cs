using Microsoft.AspNetCore.Mvc;
using PhanVanDien_241230675.Models;
using System.Diagnostics;

namespace PhanVanDien_241230675.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLTaiSanContext _context;
        public HomeController(QLTaiSanContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.LoaiTaiSan = _context.LoaiTaiSans.ToList();
            ViewBag.TieuDe = "Phan Van Dien";
            ViewBag.TaiSan = _context.TaiSans.Where(ts => ts.NguyenGia.HasValue && ts.NguyenGia.Value >= 20).ToList();
            return View();
        }
        public IActionResult LocTaiSan(int maLoai)
        {
            var loaiTaiSan = _context.LoaiTaiSans.FirstOrDefault(l =>l.MaLoai==maLoai);
            ViewBag.TieuDe = loaiTaiSan.TenLoai;
            var ds = _context.TaiSans.Where(l => l.MaLoai == maLoai).ToList();
            return PartialView("PhanVanDien_MainContent", ds);
        }
        public IActionResult Create()
        {
            ViewBag.LoaiTaiSan = _context.LoaiTaiSans.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(TaiSan ts)
        {
            _context.TaiSans.Add(ts);
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

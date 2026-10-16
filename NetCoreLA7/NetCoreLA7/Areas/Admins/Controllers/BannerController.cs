using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admins.Data;
using NetCoreLA7.Areas.Admins.Models;

namespace NetCoreLA7.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BannerController : Controller
    {
        private readonly AppDbContext _context;

        public BannerController(AppDbContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners
                .OrderBy(b => b.SortOrder)
                .ThenBy(b => b.Id)
                .ToListAsync();

            return View(banners);
        }

        public IActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banner banner)
        {
            if (!CheckDate(banner))
                return View(banner);

            if (!ModelState.IsValid)
                return View(banner);

            banner.CreatedDate = DateTime.Now;

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

   
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var banner = await _context.Banners.FindAsync(id);

            if (banner == null)
                return NotFound();

            return View(banner);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Banner banner)
        {
            if (id != banner.Id)
                return NotFound();

            if (!CheckDate(banner))
                return View(banner);

            if (!ModelState.IsValid)
                return View(banner);

            var existingBanner = await _context.Banners.FindAsync(id);

            if (existingBanner == null)
                return NotFound();

            existingBanner.Title = banner.Title;
            existingBanner.Image = banner.Image;
            existingBanner.Link = banner.Link;
            existingBanner.Description = banner.Description;
            existingBanner.SortOrder = banner.SortOrder;
            existingBanner.Status = banner.Status;
            existingBanner.StartDate = banner.StartDate;
            existingBanner.EndDate = banner.EndDate;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var banner = await _context.Banners
                .FirstOrDefaultAsync(b => b.Id == id);

            if (banner == null)
                return NotFound();

            return View(banner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);

            if (banner == null)
                return NotFound();

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CheckDate(Banner banner)
        {
            if (banner.StartDate.HasValue &&
                banner.EndDate.HasValue &&
                banner.EndDate < banner.StartDate)
            {
                ModelState.AddModelError(
                    nameof(banner.EndDate),
                    "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");

                return false;
            }

            return true;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreLA7.Areas.Admins.Data;
using NetCoreLA7.Areas.Admins.Models;

namespace NetCoreLA7.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs
                .Include(b => b.Category)
                .OrderByDescending(b => b.CreatedDate)
                .ThenByDescending(b => b.Id)
                .ToListAsync();

            return View(blogs);
        }

      
        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return View(blog);
            }

            if (blog.CategoryId.HasValue &&
                !await _context.Categories.AnyAsync(
                    c => c.Id == blog.CategoryId.Value))
            {
                ModelState.AddModelError(
                    nameof(blog.CategoryId),
                    "Danh mục không tồn tại.");

                await LoadCategories();
                return View(blog);
            }

            blog.CreatedDate = DateTime.Now;
            blog.UpdatedDate = null;

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();

            await LoadCategories();
            return View(blog);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return View(blog);
            }

            if (blog.CategoryId.HasValue &&
                !await _context.Categories.AnyAsync(
                    c => c.Id == blog.CategoryId.Value))
            {
                ModelState.AddModelError(
                    nameof(blog.CategoryId),
                    "Danh mục không tồn tại.");

                await LoadCategories();
                return View(blog);
            }

            var existingBlog = await _context.Blogs.FindAsync(id);

            if (existingBlog == null)
                return NotFound();

            existingBlog.CategoryId = blog.CategoryId;
            existingBlog.Title = blog.Title;
            existingBlog.Slug = blog.Slug;
            existingBlog.Image = blog.Image;
            existingBlog.Summary = blog.Summary;
            existingBlog.Content = blog.Content;
            existingBlog.Author = blog.Author;
            existingBlog.Status = blog.Status;
            existingBlog.PublishedDate = blog.PublishedDate;
            existingBlog.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategories()
        {
            ViewBag.CategoryId = new SelectList(
                await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync(),
                "Id",
                "Name");
        }
    }
}

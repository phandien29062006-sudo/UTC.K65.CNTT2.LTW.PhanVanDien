using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class LearnerController : Controller
    {
        SchoolContext db;
        private int pageSize = 3;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index(int? mid)
        {
            var learners = db.Learners
                .Include(l => l.Major)
                .AsQueryable();

            // Lọc theo ngành nếu có mid
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorId == mid);
            }

            // Tính số trang
            int pageNum = (int)Math.Ceiling(
                learners.Count() / (float)pageSize
            );

            ViewBag.pageNum = pageNum;
            ViewBag.mid = mid;

            // Lấy dữ liệu trang đầu
            var result = learners
                .Take(pageSize)
                .ToList();

            return View(result);
        }
        public IActionResult LearnerFilter(
            int? mid,
            string? keyword,
            int? pageIndex)
        {
            // Lấy toàn bộ Learner
            var learners = (IQueryable<Learner>)db.Learners;

            // Xác định trang hiện tại
            int page = (int)(pageIndex == null || pageIndex == 0
                ? 1 : pageIndex);

            // Lọc theo chuyên ngành
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorId == mid);
                ViewBag.mid = mid;
            }

            // Tìm kiếm theo tên
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                learners = learners.Where(l =>
                    l.FirstMidName.ToLower().Contains(keyword.ToLower()));

                ViewBag.keyword = keyword;
            }

            // Tính số trang
            int pageNum = (int)Math.Ceiling(
                learners.Count() / (float)pageSize
            );

            ViewBag.pageNum = pageNum;

            // Lấy dữ liệu của trang hiện tại
            var result = learners
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Include(m => m.Major)
                .ToList();

            return PartialView("LearnerTable", result);
        }
        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorId == mid)
                .Include(m => m.Major).ToList();

            return PartialView("LearnerTable", learners);
        }
        // Hiển thị form thêm
        public IActionResult Create()
        {
            ViewData["MajorId"] = new SelectList(
                db.Majors, "MajorId", "MajorName");

            return View();
        }

        // Xử lý thêm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Learner learner)
        {
            ModelState.Remove(nameof(Learner.Major));
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewData["MajorId"] = new SelectList(
                db.Majors, "MajorId", "MajorName",
                learner.MajorId);

            return View(learner);
        }

        // Hiển thị form sửa
        public IActionResult Edit(int id)
        {
            var learner = db.Learners.Find(id);

            if (learner == null)
                return NotFound();

            ViewData["MajorId"] = new SelectList(
                db.Majors, "MajorId", "MajorName",
                learner.MajorId);

            return View(learner);
        }

        // Xử lý sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Learner learner)
        {
            ModelState.Remove(nameof(Learner.Major));
            if (id != learner.LearnerId)
                return NotFound();

            if (ModelState.IsValid)
            {
                db.Learners.Update(learner);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewData["MajorId"] = new SelectList(
                db.Majors, "MajorId", "MajorName",
                learner.MajorId);

            return View(learner);
        }

        // Hiển thị xác nhận xóa
        public IActionResult Delete(int id)
        {
            var learner = db.Learners
                .Include(l => l.Major)
                .FirstOrDefault(l => l.LearnerId == id);

            if (learner == null)
                return NotFound();

            return View(learner);
        }

        // Xử lý xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            ModelState.Remove(nameof(Learner.Major));
            var learner = db.Learners.Find(id);

            if (learner != null)
            {
                db.Learners.Remove(learner);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
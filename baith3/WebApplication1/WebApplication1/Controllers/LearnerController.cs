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

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index(int? mid)
        {
            if (mid == null)
            {
                var learners = db.Learners
                    .Include(m => m.Major).ToList();

                return View(learners);
            }
            else
            {
                var learners = db.Learners
                    .Where(m => m.MajorId == mid)
                    .Include(m => m.Major).ToList();

                return View(learners);
            }
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;


namespace WebApplication2.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // 8.1: Hiển thị danh sách người học
        public IActionResult Index()
        {
            var learners = db.Learners
                             .Include(l => l.Major)
                             .ToList();

            return View(learners);
        }
        // GET: Learner/Create
        public IActionResult Create()
        {
            var majors = new List<SelectListItem>();

            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorID.ToString()
                });
            }

            ViewBag.MajorID = majors;

            return View();
        }

        // POST: Learner/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            ModelState.Remove("Major");

            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID =
                new SelectList(db.Majors, "MajorID", "MajorName",
                               learner.MajorID);

            return View(learner);
        }

        // GET: Learner/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);

            if (learner == null)
            {
                return NotFound();
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName", learner.MajorID);

            return View(learner);
        }

        // POST: Learner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            ModelState.Remove("Major");

            if (ModelState.IsValid)
            {
                db.Learners.Update(learner);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName", learner.MajorID);

            return View(learner);
        }

        // GET: Learner/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learner = db.Learners
                .Include(l => l.Major)
                .FirstOrDefault(l => l.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);

            if (learner == null)
            {
                return NotFound();
            }

            db.Learners.Remove(learner);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
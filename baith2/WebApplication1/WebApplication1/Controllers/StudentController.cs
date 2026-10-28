
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        // Danh sách sinh viên lưu tạm trong bộ nhớ
        private static List<Student> listStudents = new List<Student>();

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Count > 0
                    ? listStudents.Max(x => x.Id) + 1
                    : 1;

                listStudents.Add(s);

                return RedirectToAction("Index");
            }

            SetViewBag();
            return View(s);
        }

        private void SetViewBag()
        {
            ViewBag.AllGenders = Enum.GetValues<Gender>();

            ViewBag.AllBranches = new List<SelectListItem>
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
        }
    }
}

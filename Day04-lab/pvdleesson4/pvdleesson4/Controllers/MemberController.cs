using baitest1.Models.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace baitest1.Controllers
{
    public class MemberController : Controller
    {
        //public IActionResult GetMembers()
        //{
        //    List<Member> members = new List<Member>()
        //    {
        //        new Member
        //        {
        //            MemberId = Guid.NewGuid().ToString(),
        //            Username = "member1",
        //            Fullname = "Thành viên 1",
        //            Password = "123456",
        //            Email = "tv1@gmail.com"
        //        },

        //        new Member
        //        {
        //            MemberId = Guid.NewGuid().ToString(),
        //            Username = "member2",
        //            Fullname = "Thành viên 2",
        //            Password = "123456",
        //            Email = "tv2@gmail.com"
        //        },

        //        new Member
        //        {
        //            MemberId = Guid.NewGuid().ToString(),
        //            Username = "member3",
        //            Fullname = "Thành viên 3",
        //            Password = "123456",
        //            Email = "tv3@gmail.com"
        //        },

        //        new Member
        //        {
        //            MemberId = Guid.NewGuid().ToString(),
        //            Username = "member4",
        //            Fullname = "Thành viên 4",
        //            Password = "123456",
        //            Email = "tv4@gmail.com"
        //        },

        //        new Member
        //        {
        //            MemberId = Guid.NewGuid().ToString(),
        //            Username = "member5",
        //            Fullname = "Thành viên 5",
        //            Password = "123456",
        //            Email = "tv5@gmail.com"
        //        }
        //    };

        //    ViewBag.members = members;

        //    return View();
        //}
        //public IActionResult Index1()
        //{
        //    // Tạo đối tượng Member
        //    var member = new Member();

        //    // Gán thông tin cho đối tượng
        //    member.MemberId = Guid.NewGuid().ToString();
        //    member.Username = "trinhvanchung";
        //    member.Fullname = "Trịnh Văn Chung";
        //    member.Password = "password";
        //    member.Email = "chungtrinhvan@gmail.com";
        //    ViewBag.member = member;
        //    // Truyền đối tượng controller sang View
        //    return View();
        //}
        public IActionResult Index()
        {
            // Tạo đối tượng Member
            var member = new Member();

            // Gán thông tin cho đối tượng
            member.MemberId = Guid.NewGuid().ToString();
            member.Username = "trinhvanchung";
            member.Fullname = "Trịnh Văn Chung";
            member.Password = "password";
            member.Email = "chungtrinhvan@gmail.com";

            // Truyền đối tượng model sang View
            return View(member);
        }
       
            // Khai báo danh sách member dữ liệu mẫu trong lớp MemberController
        public static readonly List<Member> members = new List<Member>()
        {
            new Member{ MemberId = Guid.NewGuid().ToString(), Username = "member1", Fullname = "Thành viên 1", Password = "123456", Email = "tv1@gmail.com" },
            new Member{ MemberId = Guid.NewGuid().ToString(), Username = "member2", Fullname = "Thành viên 2", Password = "123456", Email = "tv2@gmail.com" },
            new Member{ MemberId = Guid.NewGuid().ToString(), Username = "member3", Fullname = "Thành viên 3", Password = "123456", Email = "tv3@gmail.com" },
            new Member{ MemberId = Guid.NewGuid().ToString(), Username = "member4", Fullname = "Thành viên 4", Password = "123456", Email = "tv4@gmail.com" },
            new Member{ MemberId = Guid.NewGuid().ToString(), Username = "member5", Fullname = "Thành viên 5", Password = "123456", Email = "tv5@gmail.com" },
        };

            // Action hiển thị danh sách Member
        public IActionResult GetMembers()
        {
                ViewBag.members = members; // Gán danh sách vào ViewBag
            return View();
        }

            // Action hiển thị Form thêm mới Member (Default là GET)
        public IActionResult Create()
        {
                return View();         
        }

            // Hành động gọi tương ứng với method là POST
        [HttpPost]
        public IActionResult Create(Member member)
        {
                member.MemberId = Guid.NewGuid().ToString();
                members.Add(member);
                return RedirectToAction("GetMembers");
        }
        public IActionResult Edit(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Member updatedMember)
        {
            var member = members.FirstOrDefault(m => m.MemberId == updatedMember.MemberId);
            if (member == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin
            member.Username = updatedMember.Username;
            member.Password = updatedMember.Password;
            member.Fullname = updatedMember.Fullname;
            member.Email = updatedMember.Email;

            return RedirectToAction(nameof(GetMembers));
        }

        // ==================== DETAILS ====================
        // GET: Member/Details/id
        public IActionResult Details(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // ==================== DELETE ====================
        // GET: Member/Delete/id (Hiển thị trang xác nhận xóa)
        public IActionResult Delete(string id)
        {
            var member = members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Member/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string memberId)
        {
            var member = members.FirstOrDefault(m => m.MemberId == memberId);
            if (member != null)
            {
                members.Remove(member);
            }
            return RedirectToAction(nameof(GetMembers));
        }

    }
}
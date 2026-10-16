using Microsoft.AspNetCore.Mvc;
using pvdlesson3.Models;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
namespace pvdlesson3.Controllers
{
    public class AccountController : Controller
    {
        [Route("account", Name = "account")]
        public IActionResult Index()
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id = 1,
                    Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh01.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },

                new Account()
                {
                    Id = 2,
                    Name = "Trường Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh02.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },

                new Account()
                {
                    Id = 3,
                    Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh03.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                new Account()
                {
                    Id =4,
                    Name = "Phan Diễn",
                    Email = "dien@gmail.com",
                    Phone = "0445356789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh04.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 8, 25)
                },
                new Account()
                {
                    Id = 5,
                    Name = "Trần diem",
                    Email = "trsie@gmail.com",
                    Phone = "098654389",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh05.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 8, 14)
                },
                new Account()
                {
                    Id = 6,
                    Name = "Nguyễn vàng",
                    Email = "vangtran@gmail.com",
                    Phone = "098634359",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh06.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 3, 24)
                }
            };
            ViewBag.Accounts = accounts;
            return View();
        }
        [Route("ho-so-cua-toi", Name ="profile")]
        public IActionResult Profile(int id)
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    Id = 1,
                    Name = "Hoàng Anh",
                    Email = "anh@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh01.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },

                new Account()
                {
                    Id = 2,
                    Name = "Trường Giang",
                    Email = "giang@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh02.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },

                new Account()
                {
                    Id = 3,
                    Name = "Hoàng Thúy",
                    Email = "thuy@gmail.com",
                    Phone = "0986456789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh03.jpg"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(1998, 7, 15)
                },
                   new Account()
                {
                    Id =4,
                    Name = "Phan Diễn",
                    Email = "dien@gmail.com",
                    Phone = "0445356789",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh04.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 8, 25)
                },
                new Account()
                {
                    Id = 5,
                    Name = "Trần diem",
                    Email = "trsie@gmail.com",
                    Phone = "098654389",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh05.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 8, 14)
                },
                 new Account()
                {
                    Id = 6,
                    Name = "Nguyễn vàng",
                    Email = "vangtran@gmail.com",
                    Phone = "098634359",
                    Address = "Hà Nội",
                    Avatar = Url.Content("~/Avatar/anh06.webp"),
                    Gender = 1,
                    Bio = "My name is small",
                    Birthday = new DateTime(2003, 3, 24)
                }
            };
            Account account = accounts.FirstOrDefault(x => x.Id == id);
            ViewBag.account = account;
            return View();
        }
    }
}

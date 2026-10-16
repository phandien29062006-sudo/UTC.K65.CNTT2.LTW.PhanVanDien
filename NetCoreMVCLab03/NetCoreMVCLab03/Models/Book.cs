using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace NetCoreMVCLab03.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int TotalPage { get; set; }
        public string Summary { get; set; }
        public static List<Book> GetBookList()
        {
            List<Book> books = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Title = "Chí Phèo",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "~/images/products/b1.webp",
                    Price = 500000,
                    TotalPage = 200,
                    Summary = "Tác phẩm Chí Phèo"
                },

                new Book()
                {
                    Id = 2,
                    Title = "Lão Hạc",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "~/images/products/b2.jpg",
                    Price = 700000,
                    TotalPage = 150,
                    Summary = "Tác phẩm Lão Hạc"
                },

                new Book()
                {
                    Id = 4,
                    Title = "Conan Phiêu lưu ký",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "~/images/products/b3.jpg",
                    Price = 550000,
                    TotalPage = 180,
                    Summary = "Truyện Conan"
                },

                new Book()
                {
                    Id = 6,
                    Title = "Đường Xưa Mây Trắng",
                    AuthorId = 1,
                    GenreId = 1,
                    Image = "~/images/products/b4.webp",
                    Price = 850000,
                    TotalPage = 300,
                    Summary = "Đường Xưa Mây Trắng"
                }
            };

            return books;
        }
        public  Book GetBookById(int id)
        {
            return GetBookList().FirstOrDefault(x => x.Id == id);
        }
        public List<SelectListItem> Authors { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Nam Cao" },
            new SelectListItem { Value = "2", Text = "Ngô Tất Tố" },
            new SelectListItem { Value = "3", Text = "Adamkhoom" },
            new SelectListItem { Value = "4", Text = "Thiền sư Thích Nhất Hạnh" }
        };

        public List<SelectListItem> Genres { get; } = new List<SelectListItem> 
        {
            new SelectListItem { Value = "1", Text = "Truyện tranh" },
            new SelectListItem { Value = "2", Text = "Văn học đương đại" },
            new SelectListItem { Value = "3", Text = "Phật học phổ thông" },
            new SelectListItem { Value = "4", Text = "Truyện cười" }
        };
    }
}
    
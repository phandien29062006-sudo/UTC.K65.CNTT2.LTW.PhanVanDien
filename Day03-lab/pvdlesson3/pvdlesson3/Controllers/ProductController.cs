using Microsoft.AspNetCore.Mvc;
using pvdlesson3.Models;
namespace pvdlesson3.Controllers
{
    public class ProductController : Controller
    {
        [Route("san-pham", Name = "product")]
        public IActionResult Index(int? categoryId)
        {
            List<Caterogy> categories = new List<Caterogy>
            {
                new Caterogy(){
                    Id=1,Name="Quần áo"
                },
                new Caterogy()
                {
                    Id=2,Name="Túi xách"
                },
                new Caterogy()
                {
                    Id=3,Name="Đồng hồ"
                },
                new Caterogy()
                {
                    Id=4,Name="Ti vi"
                },
                new  Caterogy()
                {
                    Id=5,Name="Tủ lạnh"
                },
                new Caterogy()
                {
                    Id=6,Name="Máy bơm"
                },
                new Caterogy()
                {
                    Id=7,Name="Quạt điện"
                },
                new Caterogy()
                {
                    Id=8,Name="Lò sưởi"
                }
            };
            List<Product> products = new List<Product>
            {
                 new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = Url.Content("~/images/product1.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nam",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = Url.Content("~/images/product2.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nữ",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ em 3-5 tuổi",
                    Image = Url.Content("~/images/product3.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em 3-5 tuổi",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 4,
                    Name = "Bộ đồ bơi cho bé thời trang",
                    Image = Url.Content("~/images/product4.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho bé thời trang",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang mẫu mới 2021",
                    Image = Url.Content("~/images/product5.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang mẫu mới 2021",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang da cá",
                    Image = Url.Content("~/images/product6.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang da cá",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },
                new Product
                {
                    Id = 7,
                    Name = "Đồng hồ nam thời trang",
                    Image = Url.Content("~/images/product7.jpg"),
                    Price = 1200000,
                    SalePrice = 950000,
                    CategoryId = 3,
                    Description = "Đồng hồ nam thời trang cao cấp",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 8,
                    Name = "Smart TV 43 inch",
                    Image = Url.Content("~/images/product8.webp"),
                    Price = 8500000,
                    SalePrice = 7500000,
                    CategoryId = 4,
                    Description = "Smart TV màn hình lớn, hình ảnh sắc nét",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 9,
                    Name = "Tủ lạnh Inverter",
                    Image = Url.Content("~/images/product9.webp"),
                    Price = 10000000,
                    SalePrice = 8900000,
                    CategoryId = 5,
                    Description = "Tủ lạnh Inverter tiết kiệm điện",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 10,
                    Name = "Quạt điện cao cấp",
                    Image = Url.Content("~/images/product10.webp"),
                    Price = 800000,
                    SalePrice = 650000,
                    CategoryId = 7,
                    Description = "Quạt điện có nhiều chế độ gió",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                }
            };
            if (categoryId.HasValue)
            {
                products = products
                    .Where(x => x.CategoryId == categoryId.Value)
                    .ToList();
            }
            ViewBag.Categories = categories;
            ViewBag.Products = products;
            return View();
        }

        public IActionResult Detail(int id)
        {
           
            List<Product> products = new List<Product>
            {
                 new Product
                {
                    Id = 1,
                    Name = "Bộ đồ bơi cho trẻ em nam",
                    Image = Url.Content("~/images/product1.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nam",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 2,
                    Name = "Bộ đồ bơi cho trẻ em nữ",
                    Image = Url.Content("~/images/product2.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em nữ",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 3,
                    Name = "Bộ đồ bơi cho trẻ em 3-5 tuổi",
                    Image = Url.Content("~/images/product3.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho trẻ em 3-5 tuổi",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 4,
                    Name = "Bộ đồ bơi cho bé thời trang",
                    Image = Url.Content("~/images/product4.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 1,
                    Description = "Bộ đồ bơi cho bé thời trang",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 5,
                    Name = "Túi thời trang mẫu mới 2021",
                    Image = Url.Content("~/images/product5.jpg"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang mẫu mới 2021",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 6,
                    Name = "Túi thời trang da cá",
                    Image = Url.Content("~/images/product6.webp"),
                    Price = 50000,
                    SalePrice = 35000,
                    CategoryId = 2,
                    Description = "Túi thời trang da cá",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },
                 new Product
                {
                    Id = 7,
                    Name = "Đồng hồ nam thời trang",
                    Image = Url.Content("~/images/product7.jpg"),
                    Price = 1200000,
                    SalePrice = 950000,
                    CategoryId = 3,
                    Description = "Đồng hồ nam thời trang cao cấp",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 8,
                    Name = "Smart TV 43 inch",
                    Image = Url.Content("~/images/product8.webp"),
                    Price = 8500000,
                    SalePrice = 7500000,
                    CategoryId = 4,
                    Description = "Smart TV màn hình lớn, hình ảnh sắc nét",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 9,
                    Name = "Tủ lạnh Inverter",
                    Image = Url.Content("~/images/product9.webp"),
                    Price = 10000000,
                    SalePrice = 8900000,
                    CategoryId = 5,
                    Description = "Tủ lạnh Inverter tiết kiệm điện",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                },

                new Product
                {
                    Id = 10,
                    Name = "Quạt điện cao cấp",
                    Image = Url.Content("~/images/product10.webp"),
                    Price = 800000,
                    SalePrice = 650000,
                    CategoryId = 7,
                    Description = "Quạt điện có nhiều chế độ gió",
                    Status = 1,
                    CreatedAt = new DateTime(2020, 12, 25)
                }
            };
            Product product = products.FirstOrDefault(x => x.Id == id);
            ViewBag.Product = product;
            return View();
        }
    }
}

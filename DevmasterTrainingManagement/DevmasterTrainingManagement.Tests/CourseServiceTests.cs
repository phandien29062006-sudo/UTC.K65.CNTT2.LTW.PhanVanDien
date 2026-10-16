using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class CourseServiceTests
    {
        private CourseService CreateService()
        {
            CourseService service = new CourseService();

            service.Add(new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C#",
                MoTa = "C# cơ bản",
                ThoiLuong = 30,
                HocPhi = 5000000
            });

            service.Add(new Course
            {
                MaKhoaHoc = "KH02",
                TenKhoaHoc = "Lập trình Java",
                MoTa = "Java cơ bản",
                ThoiLuong = 40,
                HocPhi = 6000000
            });

            return service;
        }


        // 1. Thêm khóa học thành công
        [Fact]
        public void Add_Course_ThanhCong()
        {
            CourseService service = new CourseService();

            Course course = new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C#",
                MoTa = "C# cơ bản",
                ThoiLuong = 30,
                HocPhi = 5000000
            };

            bool result = service.Add(course);

            Assert.True(result);
            Assert.Single(service.GetAll());
        }


        // 2. Không cho thêm khóa học trùng mã
        [Fact]
        public void Add_Course_TrungMa()
        {
            CourseService service = new CourseService();

            Course course1 = new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C#",
                MoTa = "C# cơ bản",
                ThoiLuong = 30,
                HocPhi = 5000000
            };

            Course course2 = new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Java",
                MoTa = "Java cơ bản",
                ThoiLuong = 40,
                HocPhi = 6000000
            };

            bool result1 = service.Add(course1);
            bool result2 = service.Add(course2);

            Assert.True(result1);
            Assert.False(result2);
            Assert.Single(service.GetAll());
        }


        // 3. Tìm khóa học theo mã
        [Fact]
        public void GetById_TimDungKhoaHoc()
        {
            CourseService service = CreateService();

            Course? result =
                service.GetById("KH01");

            Assert.NotNull(result);
            Assert.Equal("KH01", result.MaKhoaHoc);
            Assert.Equal(
                "Lập trình C#",
                result.TenKhoaHoc);
        }


        // 4. Không tìm thấy khóa học
        [Fact]
        public void GetById_KhongTimThay()
        {
            CourseService service = CreateService();

            Course? result =
                service.GetById("KH999");

            Assert.Null(result);
        }


        // 5. Cập nhật khóa học
        [Fact]
        public void Update_Course_ThanhCong()
        {
            CourseService service = CreateService();

            Course updatedCourse = new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C# nâng cao",
                MoTa = "C# nâng cao",
                ThoiLuong = 50,
                HocPhi = 7000000
            };

            bool result =
                service.Update(updatedCourse);

            Assert.True(result);

            Course? actual =
                service.GetById("KH01");

            Assert.NotNull(actual);

            Assert.Equal(
                "Lập trình C# nâng cao",
                actual.TenKhoaHoc);

            Assert.Equal(
                "C# nâng cao",
                actual.MoTa);

            Assert.Equal(
                50,
                actual.ThoiLuong);

            Assert.Equal(
                7000000,
                actual.HocPhi);
        }


        // 6. Cập nhật khóa học không tồn tại
        [Fact]
        public void Update_Course_KhongTonTai()
        {
            CourseService service = CreateService();

            Course course = new Course
            {
                MaKhoaHoc = "KH999",
                TenKhoaHoc = "Không tồn tại",
                MoTa = "Test",
                ThoiLuong = 20,
                HocPhi = 1000000
            };

            bool result =
                service.Update(course);

            Assert.False(result);
        }


        // 7. Xóa khóa học
        [Fact]
        public void Delete_Course_ThanhCong()
        {
            CourseService service = CreateService();

            bool result =
                service.Delete("KH01");

            Assert.True(result);

            Assert.Null(
                service.GetById("KH01"));

            Assert.Single(
                service.GetAll());
        }


        // 8. Xóa khóa học không tồn tại
        [Fact]
        public void Delete_Course_KhongTonTai()
        {
            CourseService service = CreateService();

            bool result =
                service.Delete("KH999");

            Assert.False(result);

            Assert.Equal(
                2,
                service.GetAll().Count);
        }


        // 9. Lấy tất cả khóa học
        [Fact]
        public void GetAll_TraVeDanhSach()
        {
            CourseService service = CreateService();

            List<Course> result =
                service.GetAll();

            Assert.Equal(2, result.Count);
        }


        // 10. Kiểm tra dữ liệu khóa học
        [Fact]
        public void GetById_KiemTraThongTin()
        {
            CourseService service = CreateService();

            Course? result =
                service.GetById("KH02");

            Assert.NotNull(result);

            Assert.Equal(
                "Lập trình Java",
                result.TenKhoaHoc);

            Assert.Equal(
                40,
                result.ThoiLuong);

            Assert.Equal(
                6000000,
                result.HocPhi);
        }
    }
}
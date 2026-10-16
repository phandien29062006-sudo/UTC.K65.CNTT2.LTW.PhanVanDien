using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class StudentServiceTests
    {
        // Test 1: Thêm học viên thành công
        [Fact]
        public void Add_Student_ThanhCong()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "nguyenvanan@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = new DateTime(2026, 8, 15)
            };

            // Act
            bool result = service.Add(student);

            // Assert
            Assert.True(result);
            Assert.Single(service.GetAll());
        }


        // Test 2: Không cho thêm học viên trùng mã
        [Fact]
        public void Add_Student_TrungMa()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student1 = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            Student student2 = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Tran Van Binh",
                NgaySinh = new DateTime(2004, 8, 20),
                DienThoai = "0902345678",
                Email = "binh@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            // Act
            bool result1 = service.Add(student1);
            bool result2 = service.Add(student2);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
            Assert.Single(service.GetAll());
        }


        // Test 3: Tìm học viên theo mã
        [Fact]
        public void GetById_TimDungHocVien()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            service.Add(student);

            // Act
            Student? result = service.GetById("HV01");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("HV01", result.MaHocVien);
            Assert.Equal("Nguyen Van An", result.HoTen);
        }


        // Test 4: Không tìm thấy học viên
        [Fact]
        public void GetById_KhongTimThay()
        {
            // Arrange
            StudentService service = new StudentService();

            // Act
            Student? result = service.GetById("HV999");

            // Assert
            Assert.Null(result);
        }


        // Test 5: Cập nhật học viên
        [Fact]
        public void Update_Student_ThanhCong()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            service.Add(student);

            Student updatedStudent = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An Updated",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0988888888",
                Email = "annew@gmail.com",
                DiaChi = "Ninh Binh",
                NgayDangKy = DateTime.Today
            };

            // Act
            bool result = service.Update(updatedStudent);

            // Assert
            Assert.True(result);

            Student? actual = service.GetById("HV01");

            Assert.NotNull(actual);
            Assert.Equal(
                "Nguyen Van An Updated",
                actual.HoTen);

            Assert.Equal(
                "0988888888",
                actual.DienThoai);

            Assert.Equal(
                "Ninh Binh",
                actual.DiaChi);
        }


        // Test 6: Cập nhật học viên không tồn tại
        [Fact]
        public void Update_Student_KhongTonTai()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV999",
                HoTen = "Hoc Vien Test",
                NgaySinh = new DateTime(2005, 1, 1),
                DienThoai = "0900000000",
                Email = "test@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            // Act
            bool result = service.Update(student);

            // Assert
            Assert.False(result);
        }


        // Test 7: Xóa học viên
        [Fact]
        public void Delete_Student_ThanhCong()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            service.Add(student);

            // Act
            bool result = service.Delete("HV01");

            // Assert
            Assert.True(result);
            Assert.Empty(service.GetAll());
        }


        // Test 8: Xóa học viên không tồn tại
        [Fact]
        public void Delete_Student_KhongTonTai()
        {
            // Arrange
            StudentService service = new StudentService();

            // Act
            bool result = service.Delete("HV999");

            // Assert
            Assert.False(result);
        }


        // Test 9: Kiểm tra trùng số điện thoại
        [Fact]
        public void IsDuplicatePhone_SoDienThoaiDaTonTai()
        {
            // Arrange
            StudentService service = new StudentService();

            Student student = new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            };

            service.Add(student);

            // Act
            bool result =
                service.IsDuplicatePhone("0901234567");

            // Assert
            Assert.True(result);
        }


        // Test 10: Kiểm tra số điện thoại chưa tồn tại
        [Fact]
        public void IsDuplicatePhone_SoDienThoaiChuaTonTai()
        {
            // Arrange
            StudentService service = new StudentService();

            // Act
            bool result =
                service.IsDuplicatePhone("0999999999");

            // Assert
            Assert.False(result);
        }


        // Test 11: Tìm kiếm học viên
        [Fact]
        public void Search_TimTheoHoTen()
        {
            // Arrange
            StudentService service = new StudentService();

            service.Add(new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            });

            service.Add(new Student
            {
                MaHocVien = "HV02",
                HoTen = "Tran Van Binh",
                NgaySinh = new DateTime(2004, 8, 20),
                DienThoai = "0902345678",
                Email = "binh@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            });

            // Act
            List<Student> result =
                service.Search("Nguyen");

            // Assert
            Assert.Single(result);
            Assert.Equal(
                "HV01",
                result[0].MaHocVien);
        }


        // Test 12: Tìm kiếm theo số điện thoại
        [Fact]
        public void Search_TimTheoSoDienThoai()
        {
            // Arrange
            StudentService service = new StudentService();

            service.Add(new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            });

            // Act
            List<Student> result =
                service.Search("0901234567");

            // Assert
            Assert.Single(result);
            Assert.Equal(
                "HV01",
                result[0].MaHocVien);
        }
    }
  
    
}
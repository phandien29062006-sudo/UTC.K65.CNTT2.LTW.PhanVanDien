using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class RegistrationServiceTests
    {
        // Tạo RegistrationService có dữ liệu mẫu
        private RegistrationService CreateService()
        {
            StudentService studentService = new StudentService();
            CourseService courseService = new CourseService();
            ClassService classService = new ClassService();

            // Thêm khóa học
            courseService.Add(new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C#",
                MoTa = "C# cơ bản",
                ThoiLuong = 30,
                HocPhi = 5000000
            });

            // Thêm học viên
            studentService.Add(new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyen Van An",
                NgaySinh = new DateTime(2005, 5, 15),
                DienThoai = "0901234567",
                Email = "an@gmail.com",
                DiaChi = "Ha Noi",
                NgayDangKy = DateTime.Today
            });

            // Thêm lớp
            classService.Add(new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(10),
                LichHoc = "T2-T4-T6",
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            });

            return new RegistrationService(
                studentService,
                classService,
                courseService);
        }


        // 1. Kiểm tra lớp còn chỗ
        [Fact]
        public void KiemTraLopConCho_LopConCho()
        {
            // Arrange
            RegistrationService service = CreateService();

            // Act
            bool result =
                service.KiemTraLopConCho("L01");

            // Assert
            Assert.True(result);
        }


        // 2. Kiểm tra mã lớp không tồn tại
        [Fact]
        public void KiemTraLopConCho_LopKhongTonTai()
        {
            // Arrange
            RegistrationService service = CreateService();

            // Act
            bool result =
                service.KiemTraLopConCho("L999");

            // Assert
            Assert.False(result);
        }


        // 3. Đăng ký học thành công
        [Fact]
        public void DangKyHoc_ThanhCong()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                NgayDangKy = DateTime.Today,
                HocPhi = 5000000
            };

            // Act
            bool result =
                service.DangKyHoc(registration);

            // Assert
            Assert.True(result);
            Assert.Single(service.GetAll());

            Assert.Equal(
                "DK01",
                service.GetAll()[0].MaDangKy);
        }


        // 4. Không cho đăng ký trùng mã đăng ký
        [Fact]
        public void DangKyHoc_TrungMaDangKy()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration1 = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            Registration registration2 = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            // Act
            bool result1 =
                service.DangKyHoc(registration1);

            bool result2 =
                service.DangKyHoc(registration2);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
            Assert.Single(service.GetAll());
        }


        // 5. Không cho học viên đăng ký cùng một lớp 2 lần
        [Fact]
        public void DangKyHoc_HocVienDaDangKy()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration1 = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration1);

            Registration registration2 = new Registration
            {
                MaDangKy = "DK02",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            // Act
            bool result =
                service.DangKyHoc(registration2);

            // Assert
            Assert.False(result);
            Assert.Single(service.GetAll());
        }


        // 6. Không cho đăng ký học viên không tồn tại
        [Fact]
        public void DangKyHoc_HocVienKhongTonTai()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV999",
                MaLop = "L01",
                HocPhi = 5000000
            };

            // Act
            bool result =
                service.DangKyHoc(registration);

            // Assert
            Assert.False(result);
            Assert.Empty(service.GetAll());
        }


        // 7. Không cho đăng ký lớp không tồn tại
        [Fact]
        public void DangKyHoc_LopKhongTonTai()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L999",
                HocPhi = 5000000
            };

            // Act
            bool result =
                service.DangKyHoc(registration);

            // Assert
            Assert.False(result);
            Assert.Empty(service.GetAll());
        }


        // 8. Tính tiền còn thiếu
        [Fact]
        public void TinhTienConThieu_Dung()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Thanh toán 2 triệu
            service.ThanhToan(
                "DK01",
                2000000);

            // Act
            decimal result =
                service.TinhTienConThieu("DK01");

            // Assert
            Assert.Equal(3000000, result);
        }


        // 9. Thanh toán một phần
        [Fact]
        public void ThanhToan_MotPhan()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Act
            bool result =
                service.ThanhToan(
                    "DK01",
                    2000000);

            // Assert
            Assert.True(result);

            Registration? actual =
                service.GetById("DK01");

            Assert.NotNull(actual);

            Assert.Equal(
                2000000,
                actual.SoTienDaDong);

            Assert.Equal(
                "Thanh toán một phần",
                actual.TrangThaiThanhToan);
        }


        // 10. Thanh toán đủ
        [Fact]
        public void ThanhToan_DuHocPhi()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Act
            bool result =
                service.ThanhToan(
                    "DK01",
                    5000000);

            // Assert
            Assert.True(result);

            Registration? actual =
                service.GetById("DK01");

            Assert.NotNull(actual);

            Assert.Equal(
                5000000,
                actual.SoTienDaDong);

            Assert.Equal(
                "Đã thanh toán",
                actual.TrangThaiThanhToan);
        }


        // 11. Không cho thanh toán quá số tiền còn thiếu
        [Fact]
        public void ThanhToan_VuotQuaHocPhi()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Act
            bool result =
                service.ThanhToan(
                    "DK01",
                    6000000);

            // Assert
            Assert.False(result);

            Registration? actual =
                service.GetById("DK01");

            Assert.NotNull(actual);

            Assert.Equal(
                0,
                actual.SoTienDaDong);
        }


        // 12. Không cho thanh toán số tiền <= 0
        [Fact]
        public void ThanhToan_SoTienKhongHopLe()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Act
            bool result =
                service.ThanhToan(
                    "DK01",
                    0);

            // Assert
            Assert.False(result);
        }


        // 13. Hủy đăng ký
        [Fact]
        public void HuyDangKy_ThanhCong()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            // Act
            bool result =
                service.HuyDangKy("DK01");

            // Assert
            Assert.True(result);

            Registration? actual =
                service.GetById("DK01");

            Assert.NotNull(actual);

            Assert.Equal(
                "Đã hủy",
                actual.TrangThaiThanhToan);
        }


        // 14. Hủy đăng ký không tồn tại
        [Fact]
        public void HuyDangKy_KhongTonTai()
        {
            // Arrange
            RegistrationService service = CreateService();

            // Act
            bool result =
                service.HuyDangKy("DK999");

            // Assert
            Assert.False(result);
        }


        // 15. Thống kê công nợ
        [Fact]
        public void ThongKeCongNo_Dung()
        {
            // Arrange
            RegistrationService service = CreateService();

            Registration registration = new Registration
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaLop = "L01",
                HocPhi = 5000000
            };

            service.DangKyHoc(registration);

            service.ThanhToan(
                "DK01",
                2000000);

            // Act
            decimal result =
                service.ThongKeCongNo();

            // Assert
            Assert.Equal(
                3000000,
                result);
        }
    }
}
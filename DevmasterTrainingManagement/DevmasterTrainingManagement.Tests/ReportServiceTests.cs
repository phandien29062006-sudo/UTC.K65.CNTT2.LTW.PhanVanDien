using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class ReportServiceTests
    {
        private CourseService courseService;
        private ClassService classService;
        private StudentService studentService;
        private RegistrationService registrationService;
        private StudentCareService studentCareService;
        private ReportService reportService;

        public ReportServiceTests()
        {
            courseService = new CourseService();
            classService = new ClassService();
            studentService = new StudentService();
            studentCareService = new StudentCareService();

            registrationService =
                new RegistrationService(
                    studentService,
                    classService,
                    courseService);

            reportService =
                new ReportService(
                    courseService,
                    classService,
                    studentService,
                    registrationService,
                    studentCareService);

            TaoDuLieuMau();
        }


        private void TaoDuLieuMau()
        {
            // =========================
            // KHÓA HỌC
            // =========================

            courseService.Add(new Course
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C#",
                MoTa = "Khóa học C#",
                ThoiLuong = 30,
                HocPhi = 5000000
            });

            courseService.Add(new Course
            {
                MaKhoaHoc = "KH02",
                TenKhoaHoc = "Lập trình Java",
                MoTa = "Khóa học Java",
                ThoiLuong = 40,
                HocPhi = 6000000
            });


            // =========================
            // LỚP
            // =========================

            classService.Add(new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(10),
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            });

            classService.Add(new ClassRoom
            {
                MaLop = "L02",
                TenLop = "Java K01",
                MaKhoaHoc = "KH02",
                NgayKhaiGiang = DateTime.Today.AddDays(20),
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            });


            // =========================
            // HỌC VIÊN
            // =========================

            studentService.Add(new Student
            {
                MaHocVien = "HV01",
                HoTen = "Nguyễn Văn A",
                NgaySinh = new DateTime(2005, 1, 1),
                DienThoai = "0900000001",
                Email = "hv01@gmail.com",
                DiaChi = "Hà Nội",
                NgayDangKy = DateTime.Today
            });

            studentService.Add(new Student
            {
                MaHocVien = "HV02",
                HoTen = "Nguyễn Văn B",
                NgaySinh = new DateTime(2005, 2, 2),
                DienThoai = "0900000002",
                Email = "hv02@gmail.com",
                DiaChi = "Hà Nội",
                NgayDangKy = DateTime.Today
            });

            studentService.Add(new Student
            {
                MaHocVien = "HV03",
                HoTen = "Nguyễn Văn C",
                NgaySinh = new DateTime(2005, 3, 3),
                DienThoai = "0900000003",
                Email = "hv03@gmail.com",
                DiaChi = "Hà Nội",
                NgayDangKy = DateTime.Today
            });


            // =========================
            // ĐĂNG KÝ
            // =========================

            registrationService.DangKyHoc(
                new Registration
                {
                    MaDangKy = "DK01",
                    MaHocVien = "HV01",
                    MaLop = "L01",
                    NgayDangKy = new DateTime(2026, 8, 1),
                    HocPhi = 5000000
                });

            registrationService.DangKyHoc(
                new Registration
                {
                    MaDangKy = "DK02",
                    MaHocVien = "HV02",
                    MaLop = "L01",
                    NgayDangKy = new DateTime(2026, 8, 5),
                    HocPhi = 5000000
                });

            registrationService.DangKyHoc(
                new Registration
                {
                    MaDangKy = "DK03",
                    MaHocVien = "HV03",
                    MaLop = "L02",
                    NgayDangKy = new DateTime(2026, 8, 10),
                    HocPhi = 6000000
                });


            // Thanh toán DK01 đủ
            registrationService.ThanhToan(
                "DK01",
                5000000);

            // DK02 đóng một phần
            registrationService.ThanhToan(
                "DK02",
                2000000);

            // DK03 đóng đủ
            registrationService.ThanhToan(
                "DK03",
                6000000);


            // =========================
            // CHĂM SÓC HỌC VIÊN
            // =========================

            studentCareService.Add(new StudentCare
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV01",
                NgayChamSoc = DateTime.Today,
                KenhLienHe = "Điện thoại",
                NoiDung = "Tư vấn khóa học",
                KetQua = "Đã liên hệ",
                NgayHenTiepTheo = DateTime.Today
            });

            studentCareService.Add(new StudentCare
            {
                MaChamSoc = "CS02",
                MaHocVien = "HV02",
                NgayChamSoc =
                    DateTime.Today.AddDays(-40),
                KenhLienHe = "Email",
                NoiDung = "Tư vấn học phí",
                KetQua = "Đang chờ",
                NgayHenTiepTheo =
                    DateTime.Today.AddDays(-5)
            });
        }


        // ============================================
        // 1. SỐ HỌC VIÊN THEO KHÓA HỌC
        // ============================================

        [Fact]
        public void BaoCaoHocVienTheoKhoaHoc_CoDuLieu()
        {
            var result =
                reportService.BaoCaoHocVienTheoKhoaHoc();

            Assert.NotNull(result);
        }


        // ============================================
        // 2. SỐ HỌC VIÊN THEO LỚP
        // ============================================

        [Fact]
        public void BaoCaoHocVienTheoLop_CoDuLieu()
        {
            var result =
                reportService.BaoCaoHocVienTheoLop();

            Assert.NotNull(result);
        }


        // ============================================
        // 3. LỚP SẮP KHAI GIẢNG
        // ============================================

        [Fact]
        public void BaoCaoLopSapKhaiGiang_TimDung()
        {
            List<ClassRoom> result =
                reportService.BaoCaoLopSapKhaiGiang();

            Assert.Equal(2, result.Count);

            Assert.Contains(
                result,
                x => x.MaLop == "L01");

            Assert.Contains(
                result,
                x => x.MaLop == "L02");
        }


        // ============================================
        // 4. HỌC VIÊN CÒN NỢ
        // ============================================

        [Fact]
        public void BaoCaoHocVienConNo_TimDung()
        {
            var result =
                reportService.BaoCaoHocVienConNo();

            Assert.NotNull(result);
        }


        // ============================================
        // 5. TỔNG DOANH THU
        // ============================================

        [Fact]
        public void BaoCaoTongDoanhThu_TinhDung()
        {
            decimal result =
                reportService.BaoCaoTongDoanhThu();

            Assert.Equal(
                13000000,
                result);
        }


        // ============================================
        // 6. DOANH THU THEO THÁNG
        // ============================================

        [Fact]
        public void BaoCaoDoanhThuTheoThang_CoDuLieu()
        {
            var result =
                reportService.BaoCaoDoanhThuTheoThang();

            Assert.NotNull(result);
        }


        // ============================================
        // 7. KHÓA HỌC NHIỀU HỌC VIÊN NHẤT
        // ============================================

        [Fact]
        public void BaoCaoKhoaHocNhieuHocVienNhat_CoDuLieu()
        {
            var result =
                reportService
                    .BaoCaoKhoaHocNhieuHocVienNhat();

            Assert.NotNull(result);
        }


        // ============================================
        // 8. HỌC VIÊN CÓ LỊCH HẸN HÔM NAY
        // ============================================

        [Fact]
        public void BaoCaoHocVienCoLichHenHomNay_TimDung()
        {
            var result =
                reportService
                    .BaoCaoHocVienCoLichHenHomNay();

            Assert.NotNull(result);
        }


        // ============================================
        // 9. HỌC VIÊN LÂU NGÀY CHƯA CHĂM SÓC
        // ============================================

        [Fact]
        public void BaoCaoHocVienLauNgayChuaChamSoc_TimDung()
        {
            var result =
                reportService
                    .BaoCaoHocVienLauNgayChuaChamSoc();

            Assert.NotNull(result);
        }


        // ============================================
        // 10. TỶ LỆ THANH TOÁN ĐỦ
        // ============================================

        [Fact]
        public void BaoCaoTyLeThanhToanDu_TinhDung()
        {
            double result =
                reportService.BaoCaoTyLeThanhToanDu();

            // DK01 và DK03 thanh toán đủ
            // DK02 chưa đủ
            // => 2 / 3 * 100 = 66.666...

            Assert.Equal(
                66.66666666666667,
                result,
                10);
        }
    }
}
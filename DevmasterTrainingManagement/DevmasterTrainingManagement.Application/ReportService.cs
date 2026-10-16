using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Application
{
    public class ReportService
    {
        private CourseService courseService;
        private ClassService classService;
        private StudentService studentService;
        private RegistrationService registrationService;
        private StudentCareService studentCareService;

        public ReportService(
            CourseService courseService,
            ClassService classService,
            StudentService studentService,
            RegistrationService registrationService,
            StudentCareService studentCareService)
        {
            this.courseService = courseService;
            this.classService = classService;
            this.studentService = studentService;
            this.registrationService = registrationService;
            this.studentCareService = studentCareService;
        }

        // 1.Số học viên theo khóa học
        public object BaoCaoHocVienTheoKhoaHoc()
        {
            var result =
                from course in courseService.GetAll()
                join lop in classService.GetAll()
                    on course.MaKhoaHoc equals lop.MaKhoaHoc
                join dk in registrationService.GetAll()
                    on lop.MaLop equals dk.MaLop
                group dk by new
                {
                    course.MaKhoaHoc,
                    course.TenKhoaHoc
                }
                into g
                select new
                {
                    MaKhoaHoc = g.Key.MaKhoaHoc,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    SoHocVien = g
                        .Select(x => x.MaHocVien)
                        .Distinct()
                        .Count()
                };

            return result.ToList();
        }

        // 2.Số học viên theo lớp
        public object BaoCaoHocVienTheoLop()
        {
            var result =
                from lop in classService.GetAll()
                join dk in registrationService.GetAll()
                    on lop.MaLop equals dk.MaLop
                    into dangKy
                select new
                {
                    MaLop = lop.MaLop,
                    TenLop = lop.TenLop,
                    SoHocVien = dangKy
                        .Select(x => x.MaHocVien)
                        .Distinct()
                        .Count()
                };

            return result.ToList();
        }

        // 3.Danh sách lớp sắp khai giảng
        public List<ClassRoom> BaoCaoLopSapKhaiGiang()
        {
            return classService
                .GetAll()
                .Where(c =>
                    c.NgayKhaiGiang >= DateTime.Today)
                .OrderBy(c => c.NgayKhaiGiang)
                .ToList();
        }

        // 4.Danh sách học viên còn nợ học phí
        public object BaoCaoHocVienConNo()
        {
            var result =
                from dk in registrationService.GetAll()
                join hv in studentService.GetAll()
                    on dk.MaHocVien equals hv.MaHocVien
                where dk.SoTienDaDong < dk.HocPhi
                select new
                {
                    MaHocVien = hv.MaHocVien,
                    HoTen = hv.HoTen,
                    MaLop = dk.MaLop,
                    HocPhi = dk.HocPhi,
                    SoTienDaDong = dk.SoTienDaDong,
                    ConNo = dk.HocPhi - dk.SoTienDaDong
                };

            return result.ToList();
        }

        // 5.Tổng doanh thu
        public decimal BaoCaoTongDoanhThu()
        {
            return registrationService
                .GetAll()
                .Sum(dk => dk.SoTienDaDong);
        }

        // 6.Doanh thu theo tháng
        public object BaoCaoDoanhThuTheoThang()
        {
            var result =
                registrationService
                    .GetAll()
                    .GroupBy(dk => new
                    {
                        Nam = dk.NgayDangKy.Year,
                        Thang = dk.NgayDangKy.Month
                    })
                    .OrderBy(g => g.Key.Nam)
                    .ThenBy(g => g.Key.Thang)
                    .Select(g => new
                    {
                        Nam = g.Key.Nam,
                        Thang = g.Key.Thang,
                        DoanhThu = g.Sum(x => x.SoTienDaDong)
                    });

            return result.ToList();
        }

        // 7.Khóa học có nhiều học viên nhất
        public object BaoCaoKhoaHocNhieuHocVienNhat()
        {
            var result =
                from course in courseService.GetAll()
                join lop in classService.GetAll()
                    on course.MaKhoaHoc equals lop.MaKhoaHoc
                join dk in registrationService.GetAll()
                    on lop.MaLop equals dk.MaLop
                group dk by new
                {
                    course.MaKhoaHoc,
                    course.TenKhoaHoc
                }
                into g
                select new
                {
                    MaKhoaHoc = g.Key.MaKhoaHoc,
                    TenKhoaHoc = g.Key.TenKhoaHoc,
                    SoHocVien = g
                        .Select(x => x.MaHocVien)
                        .Distinct()
                        .Count()
                };

            return result
                .Cast<dynamic>()
                .OrderByDescending(x => x.SoHocVien)
                .Take(1)
                .ToList();
        }

        // 8.Học viên có lịch hẹn hôm nay
        public object BaoCaoHocVienCoLichHenHomNay()
        {
            DateTime today = DateTime.Today;

            var result =
                from care in studentCareService.GetAll()
                join hv in studentService.GetAll()
                    on care.MaHocVien equals hv.MaHocVien
                where care.NgayHenTiepTheo.HasValue
                    && care.NgayHenTiepTheo.Value.Date == today
                select new
                {
                    MaHocVien = hv.MaHocVien,
                    HoTen = hv.HoTen,
                    DienThoai = hv.DienThoai,
                    NoiDung = care.NoiDung,
                    NgayHen = care.NgayHenTiepTheo
                };

            return result.ToList();
        }

        // 9.Học viên lâu ngày chưa được chăm sóc ( Quy ước: 30 ngày )
        public object BaoCaoHocVienLauNgayChuaChamSoc()
        {
            DateTime mocThoiGian =
                DateTime.Today.AddDays(-30);

            var result =
                from hv in studentService.GetAll()
                let lastCare = studentCareService
                    .GetAll()
                    .Where(c => c.MaHocVien == hv.MaHocVien)
                    .OrderByDescending(c => c.NgayChamSoc)
                    .FirstOrDefault()
                where lastCare == null
                    || lastCare.NgayChamSoc < mocThoiGian
                select new
                {
                    MaHocVien = hv.MaHocVien,
                    HoTen = hv.HoTen,
                    DienThoai = hv.DienThoai,
                    NgayChamSocGanNhat =
                        lastCare == null
                            ? (DateTime?)null
                            : lastCare.NgayChamSoc
                };

            return result.ToList();
        }

        // 10.Tỷ lệ học viên đã thanh toán đủ
        public double BaoCaoTyLeThanhToanDu()
        {
            var registrations =
                registrationService.GetAll();

            if (registrations.Count == 0)
            {
                return 0;
            }

            int daThanhToanDu =
                registrations.Count(
                    dk => dk.SoTienDaDong >= dk.HocPhi);

            return (double)daThanhToanDu
                / registrations.Count * 100;
        }
    }
}
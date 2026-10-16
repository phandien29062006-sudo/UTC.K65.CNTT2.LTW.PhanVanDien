using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Application
{
    public class RegistrationService
    {
        private List<Registration> registrations = new List<Registration>();

        private StudentService studentService;
        private ClassService classService;
        private CourseService courseService;

        public RegistrationService(
            StudentService studentService,
            ClassService classService,
            CourseService courseService)
        {
            this.studentService = studentService;
            this.classService = classService;
            this.courseService = courseService;
        }

        public List<Registration> GetAll()
        {
            return registrations;
        }

        public Registration? GetById(string maDangKy)
        {
            return registrations.FirstOrDefault(
                r => r.MaDangKy == maDangKy);
        }

     
        public bool KiemTraLopConCho(string maLop)
        {
            ClassRoom? classRoom =
                classService.GetById(maLop);

            if (classRoom == null)
            {
                return false;
            }

            int soHocVien =
                registrations.Count(r =>
                    r.MaLop == maLop &&
                    r.TrangThaiThanhToan != "Đã hủy");

            return soHocVien < classRoom.SiSoToiDa;
        }

        public bool DaDangKy(string maHocVien, string maLop)
        {
            return registrations.Any(r =>
                r.MaHocVien == maHocVien &&
                r.MaLop == maLop &&
                r.TrangThaiThanhToan != "Đã hủy");
        }

    
        public bool DangKyHoc(Registration registration)
        {
   
            if (GetById(registration.MaDangKy) != null)
            {
                return false;
            }

       
            Student? student =
                studentService.GetById(registration.MaHocVien);

            if (student == null)
            {
                return false;
            }

    
            ClassRoom? classRoom =
                classService.GetById(registration.MaLop);

            if (classRoom == null)
            {
                return false;
            }

    
            if (classRoom.TrangThai == "Đã đóng" ||
                classRoom.TrangThai == "Đã hủy")
            {
                return false;
            }

            if (DaDangKy(
                registration.MaHocVien,
                registration.MaLop))
            {
                return false;
            }

            if (!KiemTraLopConCho(registration.MaLop))
            {
                return false;
            }

    
            if (registration.NgayDangKy == default)
            {
                registration.NgayDangKy = DateTime.Now;
            }

            registration.SoTienDaDong = 0;
            registration.TrangThaiThanhToan =
                "Chưa thanh toán";

            registrations.Add(registration);

            return true;
        }

        public decimal TinhTienConThieu(string maDangKy)
        {
            Registration? registration =
                GetById(maDangKy);

            if (registration == null)
            {
                return 0;
            }

            decimal conThieu =
                registration.HocPhi -
                registration.SoTienDaDong;

            if (conThieu < 0)
            {
                conThieu = 0;
            }

            return conThieu;
        }

        public bool ThanhToan(
            string maDangKy,
            decimal soTien)
        {
            Registration? registration =
                GetById(maDangKy);

            if (registration == null)
            {
                return false;
            }

            if (registration.TrangThaiThanhToan == "Đã hủy")
            {
                return false;
            }

            if (soTien <= 0)
            {
                return false;
            }

            decimal conThieu =
                TinhTienConThieu(maDangKy);

            if (soTien > conThieu)
            {
                return false;
            }

            registration.SoTienDaDong += soTien;

            if (registration.SoTienDaDong >=
                registration.HocPhi)
            {
                registration.SoTienDaDong =
                    registration.HocPhi;

                registration.TrangThaiThanhToan =
                    "Đã thanh toán";
            }
            else
            {
                registration.TrangThaiThanhToan =
                    "Thanh toán một phần";
            }

            return true;
        }


        public bool HuyDangKy(string maDangKy)
        {
            Registration? registration =
                GetById(maDangKy);

            if (registration == null)
            {
                return false;
            }

            registration.TrangThaiThanhToan =
                "Đã hủy";

            return true;
        }


        public decimal ThongKeCongNo()
        {
            return registrations
                .Where(r =>
                    r.TrangThaiThanhToan != "Đã hủy")
                .Sum(r =>
                    r.HocPhi - r.SoTienDaDong);
        }
        public void HienThi()
        {
            Console.WriteLine(
                "\n================ DANH SÁCH ĐĂNG KÝ ================");

            if (registrations.Count == 0)
            {
                Console.WriteLine("Chưa có đăng ký nào.");
                return;
            }

            foreach (Registration r in registrations)
            {
                Console.WriteLine(
                    $"Mã ĐK: {r.MaDangKy} | " +
                    $"Học viên: {r.MaHocVien} | " +
                    $"Lớp: {r.MaLop} | " +
                    $"Ngày ĐK: {r.NgayDangKy:dd/MM/yyyy} | " +
                    $"Học phí: {r.HocPhi:N0} | " +
                    $"Đã đóng: {r.SoTienDaDong:N0} | " +
                    $"Còn thiếu: {TinhTienConThieu(r.MaDangKy):N0} | " +
                    $"Trạng thái: {r.TrangThaiThanhToan}");
            }
        }
        public void LoadData(List<Registration> data)
        {
            registrations.Clear();

            foreach (Registration registration in data)
            {
                registrations.Add(registration);
            }
        }
    }
}
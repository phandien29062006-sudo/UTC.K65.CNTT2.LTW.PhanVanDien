using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;
using DevmasterTrainingManagement.Infrastructure;

namespace DevmasterTrainingManagement.ConsoleUI
{
    internal class Program
    {
        static CourseService courseService = new CourseService();
        static ClassService classService = new ClassService();
        static StudentService studentService = new StudentService();
        static RegistrationService registrationService =
            new RegistrationService(
                studentService,
                classService,
                courseService);

        static StudentCareService studentCareService = new StudentCareService();
        static ReportService reportService =
            new ReportService(
                courseService,
                classService,
                studentService,
                registrationService,
                studentCareService);
        static StorageService storageService =
            new StorageService();
        static CsvService csvService =
            new CsvService();
        static BackupService backupService = new BackupService();

        static LogService logService =
            new LogService("error.log");
        static void LuuDuLieu()
        {
            Console.WriteLine("\n===== LƯU DỮ LIỆU =====");

            bool students =
                storageService.SaveJson(
                    "students.json",
                    studentService.GetAll());

            bool courses =
                storageService.SaveJson(
                    "courses.json",
                    courseService.GetAll());

            bool classes =
                storageService.SaveJson(
                    "classes.json",
                    classService.GetAll());

            bool registrations =
                storageService.SaveJson(
                    "registrations.json",
                    registrationService.GetAll());

            bool studentCares =
                storageService.SaveJson(
                    "studentcares.json",
                    studentCareService.GetAll());

            if (students &&
                courses &&
                classes &&
                registrations &&
                studentCares)
            {
                Console.WriteLine(
                    "Đã lưu toàn bộ dữ liệu thành công!");
            }
            else
            {
                Console.WriteLine(
                    "Có lỗi khi lưu dữ liệu!");
            }
        }
        static void SaoLuuDuLieu()
        {
            Console.Clear();

            Console.WriteLine(
                "===== SAO LƯU DỮ LIỆU =====");

            bool result =
                backupService.BackupAll(
                    ".",
                    "Backup");

            if (result)
            {
                Console.WriteLine(
                    "Đã sao lưu dữ liệu thành công!");

                Console.WriteLine(
                    "Dữ liệu được lưu trong thư mục: Backup");
            }
            else
            {
                Console.WriteLine(
                    "Sao lưu dữ liệu thất bại!");

                logService.LogError(
                    "Không thể sao lưu dữ liệu.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void KhoiPhucDuLieu()
        {
            Console.Clear();

            Console.WriteLine(
                "===== KHÔI PHỤC DỮ LIỆU =====");

            bool result =
                backupService.RestoreAll(
                    "Backup",
                    ".");

            if (result)
            {
                Console.WriteLine(
                    "Đã khôi phục dữ liệu thành công!");

                // Đọc lại dữ liệu vào chương trình
                DocDuLieu();
            }
            else
            {
                Console.WriteLine(
                    "Khôi phục dữ liệu thất bại!");

                logService.LogError(
                    "Không thể khôi phục dữ liệu.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void DocDuLieu()
        {
            Console.WriteLine("\n===== ĐỌC DỮ LIỆU =====");

            List<Student> students =
                storageService.LoadJson<Student>(
                    "students.json");

            List<Course> courses =
                storageService.LoadJson<Course>(
                    "courses.json");

            List<ClassRoom> classes =
                storageService.LoadJson<ClassRoom>(
                    "classes.json");

            List<Registration> registrations =
                storageService.LoadJson<Registration>(
                    "registrations.json");

            List<StudentCare> studentCares =
                storageService.LoadJson<StudentCare>(
                    "studentcares.json");


            studentService.LoadData(students);

            courseService.LoadData(courses);

            classService.LoadData(classes);

            registrationService.LoadData(registrations);

            studentCareService.LoadData(studentCares);


            Console.WriteLine(
                $"Đã đọc {students.Count} học viên.");

            Console.WriteLine(
                $"Đã đọc {courses.Count} khóa học.");

            Console.WriteLine(
                $"Đã đọc {classes.Count} lớp học.");

            Console.WriteLine(
                $"Đã đọc {registrations.Count} đăng ký.");

            Console.WriteLine(
                $"Đã đọc {studentCares.Count} lịch chăm sóc.");
        }
        static void ExportHocVienCsv()
        {
            Console.WriteLine("\n===== EXPORT HỌC VIÊN CSV =====");

            bool result =
                csvService.ExportCsv(
                    "students.csv",
                    studentService.GetAll());

            if (result)
            {
                Console.WriteLine(
                    "Đã export học viên ra file students.csv");
            }
            else
            {
                Console.WriteLine(
                    "Export học viên thất bại!");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ImportHocVienCsv()
        {
            Console.WriteLine("\n===== IMPORT HỌC VIÊN CSV =====");

            List<Student> students =
                csvService.ImportCsv<Student>(
                    "students.csv");

            if (students.Count == 0)
            {
                Console.WriteLine(
                    "Không có dữ liệu học viên để import.");
            }
            else
            {
                studentService.LoadData(students);

                Console.WriteLine(
                    $"Đã import {students.Count} học viên.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        static void ExportKhoaHocCsv()
        {
            Console.WriteLine("\n===== EXPORT KHÓA HỌC CSV =====");

            bool result =
                csvService.ExportCsv(
                    "courses.csv",
                    courseService.GetAll());

            if (result)
            {
                Console.WriteLine(
                    "Đã export khóa học ra file courses.csv");
            }
            else
            {
                Console.WriteLine(
                    "Export khóa học thất bại!");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ImportKhoaHocCsv()
        {
            Console.WriteLine("\n===== IMPORT KHÓA HỌC CSV =====");

            List<Course> courses =
                csvService.ImportCsv<Course>(
                    "courses.csv");

            if (courses.Count == 0)
            {
                Console.WriteLine(
                    "Không có dữ liệu khóa học để import.");
            }
            else
            {
                courseService.LoadData(courses);

                Console.WriteLine(
                    $"Đã import {courses.Count} khóa học.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ExportLopCsv()
        {
            Console.WriteLine("\n===== EXPORT LỚP HỌC CSV =====");

            bool result =
                csvService.ExportCsv(
                    "classes.csv",
                    classService.GetAll());

            if (result)
            {
                Console.WriteLine(
                    "Đã export lớp học ra file classes.csv");
            }
            else
            {
                Console.WriteLine(
                    "Export lớp học thất bại!");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }

        static void ImportLopCsv()
        {
            Console.WriteLine("\n===== IMPORT LỚP HỌC CSV =====");

            List<ClassRoom> classes =
                csvService.ImportCsv<ClassRoom>(
                    "classes.csv");

            if (classes.Count == 0)
            {
                Console.WriteLine(
                    "Không có dữ liệu lớp học để import.");
            }
            else
            {
                classService.LoadData(classes);

                Console.WriteLine(
                    $"Đã import {classes.Count} lớp học.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ExportChamSocCsv()
        {
            Console.WriteLine("\n===== EXPORT LỊCH CHĂM SÓC CSV =====");

            bool result =
                csvService.ExportCsv(
                    "studentcares.csv",
                    studentCareService.GetAll());

            if (result)
            {
                Console.WriteLine(
                    "Đã export lịch chăm sóc ra file studentcares.csv");
            }
            else
            {
                Console.WriteLine(
                    "Export lịch chăm sóc thất bại!");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ImportChamSocCsv()
        {
            Console.WriteLine("\n===== IMPORT LỊCH CHĂM SÓC CSV =====");

            List<StudentCare> studentCares =
                csvService.ImportCsv<StudentCare>(
                    "studentcares.csv");

            if (studentCares.Count == 0)
            {
                Console.WriteLine(
                    "Không có dữ liệu lịch chăm sóc để import.");
            }
            else
            {
                studentCareService.LoadData(studentCares);

                Console.WriteLine(
                    $"Đã import {studentCares.Count} lịch chăm sóc.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ExportDangKyCsv()
        {
            Console.WriteLine("\n===== EXPORT ĐĂNG KÝ CSV =====");

            bool result =
                csvService.ExportCsv(
                    "registrations.csv",
                    registrationService.GetAll());

            if (result)
            {
                Console.WriteLine(
                    "Đã export đăng ký ra file registrations.csv");
            }
            else
            {
                Console.WriteLine(
                    "Export đăng ký thất bại!");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void ImportDangKyCsv()
        {
            Console.WriteLine("\n===== IMPORT ĐĂNG KÝ CSV =====");

            List<Registration> registrations =
                csvService.ImportCsv<Registration>(
                    "registrations.csv");

            if (registrations.Count == 0)
            {
                Console.WriteLine(
                    "Không có dữ liệu đăng ký để import.");
            }
            else
            {
                registrationService.LoadData(registrations);

                Console.WriteLine(
                    $"Đã import {registrations.Count} đăng ký.");
            }

            Console.WriteLine(
                "\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
        static void MenuCsv()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("          IMPORT / EXPORT CSV");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Export học viên");
                Console.WriteLine("2. Import học viên");
                Console.WriteLine("3. Export khóa học");
                Console.WriteLine("4. Import khóa học");
                Console.WriteLine("5. Export lớp học");
                Console.WriteLine("6. Import lớp học");
                Console.WriteLine("7. Export đăng ký");
                Console.WriteLine("8. Import đăng ký");
                Console.WriteLine("9. Export chăm sóc học viên");
                Console.WriteLine("10. Import chăm sóc học viên");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("========================================");

                Console.Write("Chọn: ");
                string? chon = Console.ReadLine();

                switch (chon)
                {
                    case "1":
                        ExportHocVienCsv();
                        break;

                    case "2":
                        ImportHocVienCsv();
                        break;

                    case "3":
                        ExportKhoaHocCsv();
                        break;

                    case "4":
                        ImportKhoaHocCsv();
                        break;

                    case "5":
                        ExportLopCsv();
                        break;

                    case "6":
                        ImportLopCsv();
                        break;

                    case "7":
                        ExportDangKyCsv();
                        break;

                    case "8":
                        ImportDangKyCsv();
                        break;

                    case "9":
                        ExportChamSocCsv();
                        break;

                    case "10":
                        ImportChamSocCsv();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine(
                            "Lựa chọn không hợp lệ!");

                        Console.ReadKey();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            DocDuLieu();

            Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("     DEVMaster TRAINING MANAGEMENT");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Quản lý khóa học");
                Console.WriteLine("2. Quản lý lớp học");
                Console.WriteLine("3. Quản lý học viên");
                Console.WriteLine("4. Quản lý đăng ký khóa học");
                Console.WriteLine("5. Chăm sóc học viên");   
                Console.WriteLine("6. Báo cáo");
                Console.WriteLine("7. Import / Export CSV");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("========================================");

                Console.Write("Chọn: ");
                int chon = int.Parse(Console.ReadLine());

                switch (chon)
                {
                    case 1:
                        MenuKhoaHoc();
                        break;

                    case 2:
                        MenuLopHoc();
                        break;

                    case 3:
                        MenuHocVien();
                        break;
                    case 4:
                        MenuDangKy();
                        break;
                    case 5:
                        MenuChamSocHocVien();
                        break;
                    case 6:
                        MenuBaoCao();
                        break;
                    case 7:
                        MenuCsv();
                        break;
                    case 0:
                        LuuDuLieu();
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // MENU BÁO CÁO 
        static void MenuBaoCao()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==============================================");
                Console.WriteLine("                  BÁO CÁO");
                Console.WriteLine("==============================================");

                Console.WriteLine("1. Số học viên theo khóa học");
                Console.WriteLine("2. Số học viên theo lớp");
                Console.WriteLine("3. Danh sách lớp sắp khai giảng");
                Console.WriteLine("4. Danh sách học viên còn nợ học phí");
                Console.WriteLine("5. Tổng doanh thu");
                Console.WriteLine("6. Doanh thu theo tháng");
                Console.WriteLine("7. Khóa học có nhiều học viên nhất");
                Console.WriteLine("8. Học viên có lịch hẹn hôm nay");
                Console.WriteLine("9. Học viên lâu ngày chưa được chăm sóc");
                Console.WriteLine("10. Tỷ lệ học viên đã thanh toán đủ");
                Console.WriteLine("0. Quay lại");

                Console.Write("\nChọn chức năng: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HienThiBaoCaoHocVienTheoKhoaHoc();
                        break;

                    case "2":
                        HienThiBaoCaoHocVienTheoLop();
                        break;

                    case "3":
                        HienThiBaoCaoLopSapKhaiGiang();
                        break;

                    case "4":
                        HienThiBaoCaoHocVienConNo();
                        break;

                    case "5":
                        HienThiBaoCaoTongDoanhThu();
                        break;

                    case "6":
                        HienThiBaoCaoDoanhThuTheoThang();
                        break;

                    case "7":
                        HienThiBaoCaoKhoaHocNhieuHocVienNhat();
                        break;

                    case "8":
                        HienThiBaoCaoHocVienCoLichHenHomNay();
                        break;

                    case "9":
                        HienThiBaoCaoHocVienLauNgayChuaChamSoc();
                        break;

                    case "10":
                        HienThiBaoCaoTyLeThanhToanDu();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // CHỨC NĂNG BÁO CÁO

        static void HienThiBaoCaoHocVienTheoKhoaHoc()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("     1. SỐ HỌC VIÊN THEO KHÓA HỌC");
            Console.WriteLine("==============================================");

            var result = reportService.BaoCaoHocVienTheoKhoaHoc();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            Console.WriteLine(
                "{0,-15} {1,-30} {2,15}",
                "Mã khóa học",
                "Tên khóa học",
                "Số học viên");

            Console.WriteLine(
                "------------------------------------------------------------");

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maKhoaHoc =
                    type.GetProperty("MaKhoaHoc")?
                        .GetValue(item)?.ToString() ?? "";

                string tenKhoaHoc =
                    type.GetProperty("TenKhoaHoc")?
                        .GetValue(item)?.ToString() ?? "";

                string soHocVien =
                    type.GetProperty("SoHocVien")?
                        .GetValue(item)?.ToString() ?? "0";

                Console.WriteLine(
                    "{0,-15} {1,-30} {2,15}",
                    maKhoaHoc,
                    tenKhoaHoc,
                    soHocVien);

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine("Chưa có dữ liệu học viên đăng ký khóa học.");
            }

            Console.WriteLine();
            Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoHocVienTheoLop()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("        2. SỐ HỌC VIÊN THEO LỚP");
            Console.WriteLine("==============================================");

            var result = reportService.BaoCaoHocVienTheoLop();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            Console.WriteLine(
                "{0,-12} {1,-25} {2,15}",
                "Mã lớp",
                "Tên lớp",
                "Số học viên");

            Console.WriteLine(
                "------------------------------------------------------------");

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maLop =
                    type.GetProperty("MaLop")?
                        .GetValue(item)?.ToString() ?? "";

                string tenLop =
                    type.GetProperty("TenLop")?
                        .GetValue(item)?.ToString() ?? "";

                string soHocVien =
                    type.GetProperty("SoHocVien")?
                        .GetValue(item)?.ToString() ?? "0";

                Console.WriteLine(
                    "{0,-12} {1,-25} {2,15}",
                    maLop,
                    tenLop,
                    soHocVien);

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine("Chưa có dữ liệu.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoLopSapKhaiGiang()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("        3. LỚP SẮP KHAI GIẢNG");
            Console.WriteLine("==============================================");

            var danhSach =
                reportService.BaoCaoLopSapKhaiGiang();

            if (danhSach.Count == 0)
            {
                Console.WriteLine("Không có lớp sắp khai giảng.");
            }
            else
            {
                foreach (var lop in danhSach)
                {
                    Console.WriteLine(
                        $"Mã lớp: {lop.MaLop}");

                    Console.WriteLine(
                        $"Tên lớp: {lop.TenLop}");

                    Console.WriteLine(
                        $"Mã khóa học: {lop.MaKhoaHoc}");

                    Console.WriteLine(
                        $"Ngày khai giảng: {lop.NgayKhaiGiang:dd/MM/yyyy}");

                    Console.WriteLine(
                        $"Lịch học: {lop.LichHoc}");

                    Console.WriteLine(
                        $"Sĩ số tối đa: {lop.SiSoToiDa}");

                    Console.WriteLine("----------------------------------------------");
                }
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        static void HienThiBaoCaoHocVienConNo()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("        4. HỌC VIÊN CÒN NỢ HỌC PHÍ");
            Console.WriteLine("==============================================");

            var result = reportService.BaoCaoHocVienConNo();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            Console.WriteLine(
                "{0,-12} {1,-25} {2,-10} {3,15} {4,15} {5,15}",
                "Mã HV",
                "Họ tên",
                "Mã lớp",
                "Học phí",
                "Đã đóng",
                "Còn nợ");

            Console.WriteLine(
                "--------------------------------------------------------------------------------");

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maHocVien =
                    type.GetProperty("MaHocVien")?
                        .GetValue(item)?.ToString() ?? "";

                string hoTen =
                    type.GetProperty("HoTen")?
                        .GetValue(item)?.ToString() ?? "";

                string maLop =
                    type.GetProperty("MaLop")?
                        .GetValue(item)?.ToString() ?? "";

                decimal hocPhi =
                    (decimal)(type.GetProperty("HocPhi")?
                        .GetValue(item) ?? 0m);

                decimal daDong =
                    (decimal)(type.GetProperty("SoTienDaDong")?
                        .GetValue(item) ?? 0m);

                decimal conNo =
                    (decimal)(type.GetProperty("ConNo")?
                        .GetValue(item) ?? 0m);

                Console.WriteLine(
                    "{0,-12} {1,-25} {2,-10} {3,15:N0} {4,15:N0} {5,15:N0}",
                    maHocVien,
                    hoTen,
                    maLop,
                    hocPhi,
                    daDong,
                    conNo);

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine("Không có học viên nào còn nợ học phí.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        static void HienThiBaoCaoTongDoanhThu()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("             5. TỔNG DOANH THU");
            Console.WriteLine("==============================================");

            decimal doanhThu =
                reportService.BaoCaoTongDoanhThu();

            Console.WriteLine();

            Console.WriteLine(
                $"TỔNG DOANH THU: {doanhThu:N0} VNĐ");

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoDoanhThuTheoThang()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("          6. DOANH THU THEO THÁNG");
            Console.WriteLine("==============================================");

            var result =
                reportService.BaoCaoDoanhThuTheoThang();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            Console.WriteLine(
                "{0,-10} {1,-10} {2,20}",
                "Năm",
                "Tháng",
                "Doanh thu");

            Console.WriteLine(
                "----------------------------------------------");

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string nam =
                    type.GetProperty("Nam")?
                        .GetValue(item)?.ToString() ?? "";

                string thang =
                    type.GetProperty("Thang")?
                        .GetValue(item)?.ToString() ?? "";

                decimal doanhThu =
                    (decimal)(type.GetProperty("DoanhThu")?
                        .GetValue(item) ?? 0m);

                Console.WriteLine(
                    "{0,-10} {1,-10} {2,20:N0} VNĐ",
                    nam,
                    thang,
                    doanhThu);

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine("Chưa có dữ liệu doanh thu.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoKhoaHocNhieuHocVienNhat()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("      7. KHÓA HỌC CÓ NHIỀU HỌC VIÊN NHẤT");
            Console.WriteLine("==============================================");

            var result =
                reportService.BaoCaoKhoaHocNhieuHocVienNhat();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maKhoaHoc =
                    type.GetProperty("MaKhoaHoc")?
                        .GetValue(item)?.ToString() ?? "";

                string tenKhoaHoc =
                    type.GetProperty("TenKhoaHoc")?
                        .GetValue(item)?.ToString() ?? "";

                string soHocVien =
                    type.GetProperty("SoHocVien")?
                        .GetValue(item)?.ToString() ?? "0";

                Console.WriteLine(
                    $"Mã khóa học : {maKhoaHoc}");

                Console.WriteLine(
                    $"Tên khóa học: {tenKhoaHoc}");

                Console.WriteLine(
                    $"Số học viên : {soHocVien}");

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine("Chưa có dữ liệu.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoHocVienCoLichHenHomNay()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("       8. HỌC VIÊN CÓ LỊCH HẸN HÔM NAY");
            Console.WriteLine("==============================================");

            var result =
                reportService.BaoCaoHocVienCoLichHenHomNay();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maHocVien =
                    type.GetProperty("MaHocVien")?
                        .GetValue(item)?.ToString() ?? "";

                string hoTen =
                    type.GetProperty("HoTen")?
                        .GetValue(item)?.ToString() ?? "";

                string dienThoai =
                    type.GetProperty("DienThoai")?
                        .GetValue(item)?.ToString() ?? "";

                string noiDung =
                    type.GetProperty("NoiDung")?
                        .GetValue(item)?.ToString() ?? "";

                object? ngayHen =
                    type.GetProperty("NgayHen")?
                        .GetValue(item);

                Console.WriteLine(
                    $"Mã HV      : {maHocVien}");

                Console.WriteLine(
                    $"Họ tên      : {hoTen}");

                Console.WriteLine(
                    $"Điện thoại  : {dienThoai}");

                Console.WriteLine(
                    $"Nội dung    : {noiDung}");

                Console.WriteLine(
                    $"Ngày hẹn    : {ngayHen}");

                Console.WriteLine("----------------------------------------------");

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine(
                    "Hôm nay không có học viên có lịch hẹn.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }
        static void HienThiBaoCaoHocVienLauNgayChuaChamSoc()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("  9. HỌC VIÊN LÂU NGÀY CHƯA ĐƯỢC CHĂM SÓC");
            Console.WriteLine("==============================================");

            var result =
                reportService.BaoCaoHocVienLauNgayChuaChamSoc();

            var danhSach = (System.Collections.IEnumerable)result;

            bool coDuLieu = false;

            foreach (var item in danhSach)
            {
                var type = item.GetType();

                string maHocVien =
                    type.GetProperty("MaHocVien")?
                        .GetValue(item)?.ToString() ?? "";

                string hoTen =
                    type.GetProperty("HoTen")?
                        .GetValue(item)?.ToString() ?? "";

                string dienThoai =
                    type.GetProperty("DienThoai")?
                        .GetValue(item)?.ToString() ?? "";

                object? ngayChamSoc =
                    type.GetProperty("NgayChamSocGanNhat")?
                        .GetValue(item);

                Console.WriteLine(
                    $"Mã HV       : {maHocVien}");

                Console.WriteLine(
                    $"Họ tên       : {hoTen}");

                Console.WriteLine(
                    $"Điện thoại   : {dienThoai}");

                if (ngayChamSoc == null)
                {
                    Console.WriteLine(
                        "Lần chăm sóc: Chưa từng được chăm sóc");
                }
                else
                {
                    Console.WriteLine(
                        $"Lần chăm sóc: {ngayChamSoc}");
                }

                Console.WriteLine("----------------------------------------------");

                coDuLieu = true;
            }

            if (!coDuLieu)
            {
                Console.WriteLine(
                    "Không có học viên nào thuộc danh sách.");
            }

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        static void HienThiBaoCaoTyLeThanhToanDu()
        {
            Console.Clear();

            Console.WriteLine("==============================================");
            Console.WriteLine("       10. TỶ LỆ HỌC VIÊN THANH TOÁN ĐỦ");
            Console.WriteLine("==============================================");

            double tyLe =
                reportService.BaoCaoTyLeThanhToanDu();

            Console.WriteLine();

            Console.WriteLine(
                $"Tỷ lệ học viên đã thanh toán đủ: {tyLe:F2}%");

            Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
            Console.ReadKey();
        }

        // MENU CHĂM SÓC HỌC VIÊN
        static void MenuChamSocHocVien()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== CHĂM SÓC HỌC VIÊN =====");
                Console.WriteLine("1. Ghi lịch sử chăm sóc");
                Console.WriteLine("2. Hiển thị lịch sử theo học viên");
                Console.WriteLine("3. Hiển thị lịch hẹn hôm nay");
                Console.WriteLine("4. Hiển thị lịch hẹn quá hạn");
                Console.WriteLine("5. Thống kê kết quả chăm sóc");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GhiLichSuChamSoc();
                        break;

                    case "2":
                        HienThiLichSuTheoHocVien();
                        break;

                    case "3":
                        HienThiLichHenHomNay();
                        break;

                    case "4":
                        HienThiLichHenQuaHan();
                        break;

                    case "5":
                        ThongKeKetQuaChamSoc();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // CHỨC NĂNG CHĂM SÓC HỌC VIÊN
        static void GhiLichSuChamSoc()
        {
            Console.Clear();

            Console.WriteLine("===== GHI LỊCH SỬ CHĂM SÓC =====");

            StudentCare care = new StudentCare();

            Console.Write("Mã chăm sóc: ");
            care.MaChamSoc = Console.ReadLine();

            Console.Write("Mã học viên: ");
            care.MaHocVien = Console.ReadLine();

            Console.Write("Ngày chăm sóc (dd/MM/yyyy): ");

            DateTime ngayChamSoc;

            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out ngayChamSoc))
            {
                Console.Write("Ngày không hợp lệ! Nhập lại (dd/MM/yyyy): ");
            }

            care.NgayChamSoc = ngayChamSoc;

            Console.Write("Kênh liên hệ: ");
            care.KenhLienHe = Console.ReadLine();

            Console.Write("Nội dung: ");
            care.NoiDung = Console.ReadLine();

            Console.Write("Kết quả: ");
            care.KetQua = Console.ReadLine();

            Console.Write("Ngày hẹn tiếp theo (dd/MM/yyyy, bỏ trống nếu không có): ");

            string ngayHen = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(ngayHen))
            {
                care.NgayHenTiepTheo = null;
            }
            else
            {
                DateTime ngayHenTiepTheo;

                while (!DateTime.TryParseExact(
                    ngayHen,
                    "dd/MM/yyyy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out ngayHenTiepTheo))
                {
                    Console.Write("Ngày không hợp lệ! Nhập lại (dd/MM/yyyy): ");
                    ngayHen = Console.ReadLine();
                }

                care.NgayHenTiepTheo = ngayHenTiepTheo;
            }

            if (studentCareService.Add(care))
            {
                Console.WriteLine("Ghi lịch sử chăm sóc thành công!");
            }
            else
            {
                Console.WriteLine("Mã chăm sóc đã tồn tại!");
            }

            Console.ReadKey();
        }
        static void HienThiLichSuTheoHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== LỊCH SỬ CHĂM SÓC THEO HỌC VIÊN =====");

            Console.Write("Nhập mã học viên: ");
            string maHocVien = Console.ReadLine();

            List<StudentCare> list =
                studentCareService.GetByStudent(maHocVien);

            if (list.Count == 0)
            {
                Console.WriteLine("Không có lịch sử chăm sóc!");
                Console.ReadKey();
                return;
            }

            foreach (StudentCare care in list)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã chăm sóc       : {care.MaChamSoc}");
                Console.WriteLine($"Mã học viên       : {care.MaHocVien}");
                Console.WriteLine($"Ngày chăm sóc     : {care.NgayChamSoc:dd/MM/yyyy}");
                Console.WriteLine($"Kênh liên hệ      : {care.KenhLienHe}");
                Console.WriteLine($"Nội dung          : {care.NoiDung}");
                Console.WriteLine($"Kết quả           : {care.KetQua}");

                if (care.NgayHenTiepTheo.HasValue)
                {
                    Console.WriteLine(
                        $"Ngày hẹn tiếp theo: {care.NgayHenTiepTheo.Value:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("Ngày hẹn tiếp theo: Không có");
                }
            }

            Console.ReadKey();
        }
        static void HienThiLichHenHomNay()
        {
            Console.Clear();

            Console.WriteLine("===== LỊCH HẸN HÔM NAY =====");

            List<StudentCare> list =
                studentCareService.GetTodayAppointments();

            if (list.Count == 0)
            {
                Console.WriteLine("Hôm nay không có lịch hẹn.");
                Console.ReadKey();
                return;
            }

            foreach (StudentCare care in list)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã chăm sóc : {care.MaChamSoc}");
                Console.WriteLine($"Mã học viên : {care.MaHocVien}");
                Console.WriteLine($"Ngày hẹn    : {care.NgayHenTiepTheo:dd/MM/yyyy}");
                Console.WriteLine($"Kênh liên hệ: {care.KenhLienHe}");
                Console.WriteLine($"Nội dung    : {care.NoiDung}");
                Console.WriteLine($"Kết quả     : {care.KetQua}");
            }

            Console.ReadKey();
        }
        static void HienThiLichHenQuaHan()
        {
            Console.Clear();

            Console.WriteLine("===== LỊCH HẸN QUÁ HẠN =====");

            List<StudentCare> list =
                studentCareService.GetOverdueAppointments();

            if (list.Count == 0)
            {
                Console.WriteLine("Không có lịch hẹn quá hạn.");
                Console.ReadKey();
                return;
            }

            foreach (StudentCare care in list)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã chăm sóc : {care.MaChamSoc}");
                Console.WriteLine($"Mã học viên : {care.MaHocVien}");
                Console.WriteLine($"Ngày hẹn    : {care.NgayHenTiepTheo:dd/MM/yyyy}");
                Console.WriteLine($"Kênh liên hệ: {care.KenhLienHe}");
                Console.WriteLine($"Nội dung    : {care.NoiDung}");
                Console.WriteLine($"Kết quả     : {care.KetQua}");
            }

            Console.ReadKey();
        }

        static void ThongKeKetQuaChamSoc()
        {
            Console.Clear();

            Console.WriteLine("===== THỐNG KÊ KẾT QUẢ CHĂM SÓC =====");

            Dictionary<string, int> statistics =
                studentCareService.StatisticsResult();

            if (statistics.Count == 0)
            {
                Console.WriteLine("Chưa có dữ liệu chăm sóc.");
                Console.ReadKey();
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    $"{item.Key}: {item.Value} lần");
            }

            Console.ReadKey();
        }
        // MENU ĐĂNG KÍ HỌC
        static void MenuDangKy()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("====================================");
                Console.WriteLine("       QUẢN LÝ ĐĂNG KÝ KHÓA HỌC");
                Console.WriteLine("====================================");

                Console.WriteLine("1. Đăng ký học");
                Console.WriteLine("2. Hiển thị danh sách đăng ký");
                Console.WriteLine("3. Kiểm tra lớp còn chỗ");
                Console.WriteLine("4. Kiểm tra học viên đã đăng ký lớp");
                Console.WriteLine("5. Tính số tiền còn thiếu");
                Console.WriteLine("6. Ghi nhận thanh toán");
                Console.WriteLine("7. Hủy đăng ký");
                Console.WriteLine("8. Thống kê công nợ");
                Console.WriteLine("0. Quay lại");

                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DangKyHoc();
                        break;

                    case "2":
                        HienThiDangKy();
                        break;

                    case "3":
                        KiemTraLopConCho();
                        break;

                    case "4":
                        KiemTraDaDangKy();
                        break;

                    case "5":
                        TinhTienConThieu();
                        break;

                    case "6":
                        ThanhToan();
                        break;

                    case "7":
                        HuyDangKy();
                        break;

                    case "8":
                        ThongKeCongNo();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // CHỨC NĂNG ĐĂNG KÍ HỌC 
        static void DangKyHoc()
        {
            Console.Clear();

            Console.WriteLine("===== ĐĂNG KÝ HỌC =====");

            Console.Write("Mã đăng ký: ");
            string maDangKy = Console.ReadLine();

            Console.Write("Mã học viên: ");
            string maHocVien = Console.ReadLine();

            Student? student =
                studentService.GetById(maHocVien);

            if (student == null)
            {
                Console.WriteLine("Không tìm thấy học viên!");
                Console.ReadKey();
                return;
            }

            Console.Write("Mã lớp: ");
            string maLop = Console.ReadLine();

            ClassRoom? classRoom =
                classService.GetById(maLop);

            if (classRoom == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            if (!registrationService.KiemTraLopConCho(maLop))
            {
                Console.WriteLine("Lớp đã đầy!");
                Console.ReadKey();
                return;
            }

            if (registrationService.DaDangKy(
                maHocVien, maLop))
            {
                Console.WriteLine(
                    "Học viên đã đăng ký lớp này!");
                Console.ReadKey();
                return;
            }

            Course? course =
                courseService.GetById(classRoom.MaKhoaHoc);

            if (course == null)
            {
                Console.WriteLine(
                    "Không tìm thấy khóa học!");
                Console.ReadKey();
                return;
            }

            Registration registration = new Registration
            {
                MaDangKy = maDangKy,
                MaHocVien = maHocVien,
                MaLop = maLop,
                NgayDangKy = DateTime.Now,
                HocPhi = course.HocPhi,
                SoTienDaDong = 0,
                TrangThaiThanhToan = "Chưa thanh toán"
            };

            if (registrationService.DangKyHoc(
                registration))
            {
                Console.WriteLine();
                Console.WriteLine("Đăng ký học thành công!");
                Console.WriteLine(
                    $"Học phí: {registration.HocPhi:N0} VNĐ");
            }
            else
            {
                Console.WriteLine(
                    "Đăng ký học thất bại!");
            }

            Console.ReadKey();
        }
        static void HienThiDangKy()
        {
            Console.Clear();

            Console.WriteLine("===== DANH SÁCH ĐĂNG KÝ =====");

            List<Registration> registrations =
                registrationService.GetAll();

            if (registrations.Count == 0)
            {
                Console.WriteLine(
                    "Chưa có đăng ký nào!");
                Console.ReadKey();
                return;
            }

            foreach (Registration r in registrations)
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine(
                    $"Mã đăng ký: {r.MaDangKy}");
                Console.WriteLine(
                    $"Mã học viên: {r.MaHocVien}");
                Console.WriteLine(
                    $"Mã lớp: {r.MaLop}");
                Console.WriteLine(
                    $"Ngày đăng ký: {r.NgayDangKy:dd/MM/yyyy}");
                Console.WriteLine(
                    $"Học phí: {r.HocPhi:N0} VNĐ");
                Console.WriteLine(
                    $"Đã đóng: {r.SoTienDaDong:N0} VNĐ");
                Console.WriteLine(
                    $"Còn thiếu: {r.HocPhi - r.SoTienDaDong:N0} VNĐ");
                Console.WriteLine(
                    $"Trạng thái: {r.TrangThaiThanhToan}");
            }

            Console.ReadKey();
        }
        static void KiemTraLopConCho()
        {
            Console.Clear();

            Console.WriteLine("===== KIỂM TRA LỚP CÒN CHỖ =====");

            Console.Write("Mã lớp: ");
            string maLop = Console.ReadLine();

            ClassRoom? classRoom =
                classService.GetById(maLop);

            if (classRoom == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            int soHocVien =
                registrationService.GetAll()
                .Count(r =>
                    r.MaLop == maLop &&
                    r.TrangThaiThanhToan != "Đã hủy");

            Console.WriteLine();
            Console.WriteLine(
                $"Sĩ số tối đa: {classRoom.SiSoToiDa}");

            Console.WriteLine(
                $"Số học viên hiện tại: {soHocVien}");

            Console.WriteLine(
                $"Số chỗ còn lại: {classRoom.SiSoToiDa - soHocVien}");

            if (registrationService.KiemTraLopConCho(maLop))
            {
                Console.WriteLine("Lớp còn chỗ.");
            }
            else
            {
                Console.WriteLine("Lớp đã đầy.");
            }

            Console.ReadKey();
        }
        static void KiemTraDaDangKy()
        {
            Console.Clear();

            Console.WriteLine(
                "===== KIỂM TRA ĐĂNG KÝ TRÙNG =====");

            Console.Write("Mã học viên: ");
            string maHocVien = Console.ReadLine();

            Console.Write("Mã lớp: ");
            string maLop = Console.ReadLine();

            if (registrationService.DaDangKy(
                maHocVien, maLop))
            {
                Console.WriteLine(
                    "Học viên ĐÃ đăng ký lớp này.");
            }
            else
            {
                Console.WriteLine(
                    "Học viên CHƯA đăng ký lớp này.");
            }

            Console.ReadKey();
        }
        static void TinhTienConThieu()
        {
            Console.Clear();

            Console.WriteLine(
                "===== TÍNH TIỀN CÒN THIẾU =====");

            Console.Write("Mã đăng ký: ");
            string maDangKy = Console.ReadLine();

            Registration? registration =
                registrationService.GetById(maDangKy);

            if (registration == null)
            {
                Console.WriteLine(
                    "Không tìm thấy đăng ký!");
                Console.ReadKey();
                return;
            }

            decimal conThieu =
                registrationService.TinhTienConThieu(
                    maDangKy);

            Console.WriteLine();
            Console.WriteLine(
                $"Học phí: {registration.HocPhi:N0} VNĐ");

            Console.WriteLine(
                $"Đã đóng: {registration.SoTienDaDong:N0} VNĐ");

            Console.WriteLine(
                $"Còn thiếu: {conThieu:N0} VNĐ");

            Console.ReadKey();
        }
        static void ThanhToan()
        {
            Console.Clear();

            Console.WriteLine("===== GHI NHẬN THANH TOÁN =====");

            Console.Write("Mã đăng ký: ");
            string maDangKy = Console.ReadLine();

            Registration? registration =
                registrationService.GetById(maDangKy);

            if (registration == null)
            {
                Console.WriteLine(
                    "Không tìm thấy đăng ký!");
                Console.ReadKey();
                return;
            }

            decimal conThieu =
                registrationService.TinhTienConThieu(
                    maDangKy);

            Console.WriteLine(
                $"Số tiền còn thiếu: {conThieu:N0} VNĐ");

            Console.Write("Số tiền thanh toán: ");

            if (!decimal.TryParse(
                Console.ReadLine(),
                out decimal soTien))
            {
                Console.WriteLine(
                    "Số tiền không hợp lệ!");
                Console.ReadKey();
                return;
            }

            if (registrationService.ThanhToan(
                maDangKy, soTien))
            {
                Console.WriteLine(
                    "Thanh toán thành công!");

                Console.WriteLine(
                    $"Đã đóng: {registration.SoTienDaDong:N0} VNĐ");

                Console.WriteLine(
                    $"Trạng thái: " +
                    $"{registration.TrangThaiThanhToan}");
            }
            else
            {
                Console.WriteLine(
                    "Thanh toán thất bại!");
                Console.WriteLine(
                    "Kiểm tra số tiền hoặc mã đăng ký.");
            }

            Console.ReadKey();
        }
        static void HuyDangKy()
        {
            Console.Clear();

            Console.WriteLine("===== HỦY ĐĂNG KÝ =====");

            Console.Write("Mã đăng ký: ");
            string maDangKy = Console.ReadLine();

            Registration? registration =
                registrationService.GetById(maDangKy);

            if (registration == null)
            {
                Console.WriteLine(
                    "Không tìm thấy đăng ký!");
                Console.ReadKey();
                return;
            }

            if (registrationService.HuyDangKy(
                maDangKy))
            {
                Console.WriteLine(
                    "Hủy đăng ký thành công!");
            }
            else
            {
                Console.WriteLine(
                    "Hủy đăng ký thất bại!");
            }

            Console.ReadKey();
        }
        static void ThongKeCongNo()
        {
            Console.Clear();

            Console.WriteLine("===== THỐNG KÊ CÔNG NỢ =====");

            List<Registration> registrations =
                registrationService.GetAll();

            var danhSachNo = registrations
                .Where(r =>
                    r.TrangThaiThanhToan != "Đã hủy" &&
                    r.HocPhi > r.SoTienDaDong)
                .ToList();

            if (danhSachNo.Count == 0)
            {
                Console.WriteLine(
                    "Không có học viên đang nợ học phí.");
                Console.ReadKey();
                return;
            }

            decimal tongNo = 0;

            foreach (Registration r in danhSachNo)
            {
                decimal conNo =
                    r.HocPhi - r.SoTienDaDong;

                tongNo += conNo;

                Console.WriteLine("------------------------------------");

                Console.WriteLine(
                    $"Mã đăng ký: {r.MaDangKy}");

                Console.WriteLine(
                    $"Mã học viên: {r.MaHocVien}");

                Console.WriteLine(
                    $"Mã lớp: {r.MaLop}");

                Console.WriteLine(
                    $"Học phí: {r.HocPhi:N0} VNĐ");

                Console.WriteLine(
                    $"Đã đóng: {r.SoTienDaDong:N0} VNĐ");

                Console.WriteLine(
                    $"Còn nợ: {conNo:N0} VNĐ");
            }

            Console.WriteLine("------------------------------------");

            Console.WriteLine(
                $"TỔNG CÔNG NỢ: {tongNo:N0} VNĐ");

            Console.ReadKey();
        }


        // MENU KHÓA HỌC
        static void MenuKhoaHoc()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("          QUẢN LÝ KHÓA HỌC");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Thêm khóa học");
                Console.WriteLine("2. Hiển thị danh sách");
                Console.WriteLine("3. Sửa khóa học");
                Console.WriteLine("4. Xóa khóa học");
                Console.WriteLine("5. Tìm kiếm khóa học");
                Console.WriteLine("6. Sắp xếp theo học phí");
                Console.WriteLine("7. Lọc theo trạng thái");
                Console.WriteLine("8. Thống kê học phí");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("========================================");

                Console.Write("Chọn: ");
                int chon = int.Parse(Console.ReadLine());

                switch (chon)
                {
                    case 1:
                        ThemKhoaHoc();
                        break;

                    case 2:
                        HienThiDanhSach();
                        break;

                    case 3:
                        SuaKhoaHoc();
                        break;

                    case 4:
                        XoaKhoaHoc();
                        break;

                    case 5:
                        TimKiemKhoaHoc();
                        break;

                    case 6:
                        SapXepKhoaHoc();
                        break;

                    case 7:
                        LocTheoTrangThai();
                        break;

                    case 8:
                        ThongKeHocPhi();
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // MENU LỚP HỌC
        static void MenuLopHoc()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("           QUẢN LÝ LỚP HỌC");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Tạo lớp");
                Console.WriteLine("2. Hiển thị danh sách lớp");
                Console.WriteLine("3. Cập nhật lớp");
                Console.WriteLine("4. Kiểm tra sĩ số");
                Console.WriteLine("5. Hiển thị lớp sắp khai giảng");
                Console.WriteLine("6. Hiển thị lớp đang học");
                Console.WriteLine("7. Đóng lớp");
                Console.WriteLine("8. Hủy lớp");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("========================================");

                Console.Write("Chọn: ");
                int chon = int.Parse(Console.ReadLine());

                switch (chon)
                {
                    case 1:
                        TaoLop();
                        break;

                    case 2:
                        HienThiDanhSachLop();
                        break;

                    case 3:
                        CapNhatLop();
                        break;

                    case 4:
                        KiemTraSiSo();
                        break;

                    case 5:
                        HienThiLopSapKhaiGiang();
                        break;

                    case 6:
                        HienThiLopDangHoc();
                        break;

                    case 7:
                        DongLop();
                        break;

                    case 8:
                        HuyLop();
                        break;

                    case 0:
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // MENU HỌC VIÊN
        static void MenuHocVien()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("       QUẢN LÝ HỌC VIÊN");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Thêm học viên");
                Console.WriteLine("2. Hiển thị danh sách học viên");
                Console.WriteLine("3. Sửa học viên");
                Console.WriteLine("4. Xóa học viên");
                Console.WriteLine("5. Kiểm tra trùng điện thoại");
                Console.WriteLine("6. Tìm học viên");
                Console.WriteLine("7. Import từ CSV");
                Console.WriteLine("8. Export sang CSV");
                Console.WriteLine("9. Lưu dữ liệu JSON");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine("==================================");

                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ThemHocVien();
                        break;

                    case "2":
                        HienThiHocVien();
                        break;

                    case "3":
                        SuaHocVien();
                        break;

                    case "4":
                        XoaHocVien();
                        break;

                    case "5":
                        KiemTraTrungDienThoai();
                        break;

                    case "6":
                        TimHocVien();
                        break;

                    case "7":
                        ImportCSV();
                        break;

                    case "8":
                        ExportCSV();
                        break;

                    case "9":
                        LuuJson();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // CHỨC NĂNG HỌC VIÊN
        static void ThemHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== THÊM HỌC VIÊN =====");

            Student student = new Student();

            Console.Write("Mã học viên: ");
            student.MaHocVien = Console.ReadLine();

            if (studentService.GetById(student.MaHocVien) != null)
            {
                Console.WriteLine("Mã học viên đã tồn tại!");
                Console.ReadKey();
                return;
            }

            Console.Write("Họ tên: ");
            student.HoTen = Console.ReadLine();

            Console.Write("Ngày sinh (dd/MM/yyyy): ");

            DateTime ngaySinh;

            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out ngaySinh))
            {
                Console.Write("Ngày không hợp lệ! Nhập lại: ");
            }

            student.NgaySinh = ngaySinh;

            Console.Write("Điện thoại: ");
            student.DienThoai = Console.ReadLine();

            if (studentService.IsDuplicatePhone(student.DienThoai))
            {
                Console.WriteLine("Số điện thoại đã tồn tại!");
                Console.ReadKey();
                return;
            }

            Console.Write("Email: ");
            student.Email = Console.ReadLine();

            Console.Write("Địa chỉ: ");
            student.DiaChi = Console.ReadLine();

            student.NgayDangKy = DateTime.Now;

            studentService.Add(student);

            Console.WriteLine("\nThêm học viên thành công!");
            Console.ReadKey();
        }
        static void HienThiHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== DANH SÁCH HỌC VIÊN =====");

            List<Student> students = studentService.GetAll();

            if (students.Count == 0)
            {
                Console.WriteLine("Chưa có học viên.");
                Console.ReadKey();
                return;
            }

            foreach (Student student in students)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã học viên : {student.MaHocVien}");
                Console.WriteLine($"Họ tên      : {student.HoTen}");
                Console.WriteLine($"Ngày sinh   : {student.NgaySinh:dd/MM/yyyy}");
                Console.WriteLine($"Điện thoại  : {student.DienThoai}");
                Console.WriteLine($"Email       : {student.Email}");
                Console.WriteLine($"Địa chỉ     : {student.DiaChi}");
                Console.WriteLine($"Ngày đăng ký: {student.NgayDangKy:dd/MM/yyyy}");
            }

            Console.WriteLine("----------------------------------------");

            Console.ReadKey();
        }

        static void SuaHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== SỬA HỌC VIÊN =====");

            Console.Write("Nhập mã học viên cần sửa: ");
            string maHocVien = Console.ReadLine();

            Student? student = studentService.GetById(maHocVien);

            if (student == null)
            {
                Console.WriteLine("Không tìm thấy học viên!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Họ tên hiện tại: {student.HoTen}");
            Console.Write("Họ tên mới: ");
            student.HoTen = Console.ReadLine();

            Console.WriteLine($"Ngày sinh hiện tại: {student.NgaySinh:dd/MM/yyyy}");
            Console.Write("Ngày sinh mới (dd/MM/yyyy): ");

            DateTime ngaySinh;

            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out ngaySinh))
            {
                Console.Write("Ngày không hợp lệ! Nhập lại: ");
            }

            student.NgaySinh = ngaySinh;

            Console.WriteLine($"Điện thoại hiện tại: {student.DienThoai}");
            Console.Write("Điện thoại mới: ");
            string dienThoaiMoi = Console.ReadLine();

            if (dienThoaiMoi != student.DienThoai &&
                studentService.IsDuplicatePhone(dienThoaiMoi))
            {
                Console.WriteLine("Số điện thoại đã tồn tại!");
                Console.ReadKey();
                return;
            }

            student.DienThoai = dienThoaiMoi;

            Console.Write("Email mới: ");
            student.Email = Console.ReadLine();

            Console.Write("Địa chỉ mới: ");
            student.DiaChi = Console.ReadLine();

            Console.WriteLine("\nCập nhật học viên thành công!");

            Console.ReadKey();
        }
        static void XoaHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== XÓA HỌC VIÊN =====");

            Console.Write("Nhập mã học viên cần xóa: ");
            string maHocVien = Console.ReadLine();

            Student? student = studentService.GetById(maHocVien);

            if (student == null)
            {
                Console.WriteLine("Không tìm thấy học viên!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Học viên: {student.HoTen}");

            Console.Write("Bạn có chắc muốn xóa? (Y/N): ");
            string confirm = Console.ReadLine();

            if (confirm.ToUpper() == "Y")
            {
                studentService.Delete(maHocVien);

                Console.WriteLine("Xóa học viên thành công!");
            }
            else
            {
                Console.WriteLine("Đã hủy xóa.");
            }

            Console.ReadKey();
        }
        static void KiemTraTrungDienThoai()
        {
            Console.Clear();

            Console.WriteLine("===== KIỂM TRA TRÙNG ĐIỆN THOẠI =====");

            Console.Write("Nhập số điện thoại: ");
            string dienThoai = Console.ReadLine();

            if (studentService.IsDuplicatePhone(dienThoai))
            {
                Console.WriteLine("Số điện thoại đã tồn tại!");
            }
            else
            {
                Console.WriteLine("Số điện thoại chưa tồn tại.");
            }

            Console.ReadKey();
        }
        static void TimHocVien()
        {
            Console.Clear();

            Console.WriteLine("===== TÌM HỌC VIÊN =====");

            Console.Write("Nhập tên, điện thoại hoặc email: ");
            string keyword = Console.ReadLine();

            List<Student> result =
                studentService.Search(keyword);

            if (result.Count == 0)
            {
                Console.WriteLine("Không tìm thấy học viên!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n===== KẾT QUẢ TÌM KIẾM =====");

            foreach (Student student in result)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã học viên : {student.MaHocVien}");
                Console.WriteLine($"Họ tên      : {student.HoTen}");
                Console.WriteLine($"Ngày sinh   : {student.NgaySinh:dd/MM/yyyy}");
                Console.WriteLine($"Điện thoại  : {student.DienThoai}");
                Console.WriteLine($"Email       : {student.Email}");
                Console.WriteLine($"Địa chỉ     : {student.DiaChi}");
                Console.WriteLine($"Ngày đăng ký: {student.NgayDangKy:dd/MM/yyyy}");
            }

            Console.ReadKey();
        }
        static void ImportCSV()
        {
            Console.Clear();

            Console.WriteLine("===== IMPORT CSV =====");

            Console.Write("Nhập đường dẫn file CSV: ");
            string filePath = Console.ReadLine();

            if (studentService.ImportCSV(filePath))
            {
                Console.WriteLine("Import CSV thành công!");
            }
            else
            {
                Console.WriteLine("Import CSV thất bại!");
            }

            Console.ReadKey();
        }
        static void ExportCSV()
        {
            Console.Clear();

            Console.WriteLine("===== EXPORT CSV =====");

            Console.Write("Nhập đường dẫn lưu file CSV: ");
            string filePath = Console.ReadLine();

            if (studentService.ExportCSV(filePath))
            {
                Console.WriteLine("Export CSV thành công!");
            }
            else
            {
                Console.WriteLine("Export CSV thất bại!");
            }

            Console.ReadKey();
        }
        static void LuuJson()
        {
            Console.Clear();

            Console.WriteLine("===== LƯU DỮ LIỆU JSON =====");

            Console.Write("Nhập đường dẫn file JSON: ");
            string filePath = Console.ReadLine();

            if (studentService.SaveJson(filePath))
            {
                Console.WriteLine("Lưu JSON thành công!");
            }
            else
            {
                Console.WriteLine("Lưu JSON thất bại!");
            }

            Console.ReadKey();
        }

        // CHỨC NĂNG LỚP HỌC

        static void TaoLop()
        {
            Console.Clear();

            Console.WriteLine("===== TẠO LỚP =====");

            ClassRoom c = new ClassRoom();

            Console.Write("Mã lớp: ");
            c.MaLop = Console.ReadLine();

            Console.Write("Tên lớp: ");
            c.TenLop = Console.ReadLine();

            Console.Write("Mã khóa học: ");
            c.MaKhoaHoc = Console.ReadLine();

            Console.Write("Ngày khai giảng (dd/MM/yyyy): ");

            DateTime ngayKhaiGiang;

            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out ngayKhaiGiang))
            {
                Console.Write("Ngày không hợp lệ! Nhập lại (dd/MM/yyyy): ");
            }

            c.NgayKhaiGiang = ngayKhaiGiang;

            Console.Write("Lịch học: ");
            c.LichHoc = Console.ReadLine();

            Console.Write("Sĩ số tối đa: ");
            c.SiSoToiDa = int.Parse(
                Console.ReadLine());

            Console.Write("Trạng thái: ");
            c.TrangThai = Console.ReadLine();

            bool result = classService.Add(c);

            if (result)
            {
                Console.WriteLine();
                Console.WriteLine("Tạo lớp thành công!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Mã lớp đã tồn tại!");
            }

            Console.ReadKey();
        }

        static void HienThiDanhSachLop()
        {
            Console.Clear();

            List<ClassRoom> ds = classService.GetAll();

            Console.WriteLine("========================================");
            Console.WriteLine("          DANH SÁCH LỚP HỌC");
            Console.WriteLine("========================================");

            if (ds.Count == 0)
            {
                Console.WriteLine("Chưa có lớp học nào!");
                Console.ReadKey();
                return;
            }

            foreach (ClassRoom c in ds)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã lớp          : {c.MaLop}");
                Console.WriteLine($"Tên lớp         : {c.TenLop}");
                Console.WriteLine($"Mã khóa học     : {c.MaKhoaHoc}");
                Console.WriteLine($"Ngày khai giảng : {c.NgayKhaiGiang:dd/MM/yyyy}");
                Console.WriteLine($"Lịch học        : {c.LichHoc}");
                Console.WriteLine($"Sĩ số tối đa    : {c.SiSoToiDa}");
                Console.WriteLine($"Trạng thái      : {c.TrangThai}");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Tổng số lớp: {ds.Count}");

            Console.ReadKey();
        }

        static void CapNhatLop()
        {
            Console.Clear();

            Console.WriteLine("===== CẬP NHẬT LỚP =====");

            Console.Write("Nhập mã lớp cần cập nhật: ");
            string maLop = Console.ReadLine();

            ClassRoom? oldClass = classService.GetById(maLop);

            if (oldClass == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            Console.Write("Tên lớp mới: ");
            oldClass.TenLop = Console.ReadLine();

            Console.Write("Mã khóa học mới: ");
            oldClass.MaKhoaHoc = Console.ReadLine();

            Console.Write("Ngày khai giảng (dd/MM/yyyy): ");

            DateTime ngayKhaiGiang;

            while (!DateTime.TryParseExact(
                Console.ReadLine(),
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out ngayKhaiGiang))
            {
                Console.Write("Ngày không hợp lệ! Nhập lại (dd/MM/yyyy): ");
            }

            oldClass.NgayKhaiGiang = ngayKhaiGiang;

            Console.Write("Lịch học mới: ");
            oldClass.LichHoc = Console.ReadLine();

            Console.Write("Sĩ số tối đa mới: ");
            oldClass.SiSoToiDa =
                int.Parse(Console.ReadLine());

            Console.Write("Trạng thái mới: ");
            oldClass.TrangThai = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Cập nhật lớp thành công!");

            Console.ReadKey();
        }

        static void KiemTraSiSo()
        {
            Console.Clear();

            Console.WriteLine("===== KIỂM TRA SĨ SỐ =====");

            Console.Write("Mã lớp: ");
            string maLop = Console.ReadLine();

            ClassRoom? lop = classService.GetById(maLop);

            if (lop == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            Console.Write("Nhập số học viên hiện tại: ");
            int soHocVien = int.Parse(
                Console.ReadLine());

            bool conCho =
                classService.KiemTraSiSo(
                    maLop,
                    soHocVien);

            Console.WriteLine();

            if (conCho)
            {
                Console.WriteLine("Lớp vẫn còn chỗ.");
                Console.WriteLine(
                    $"Còn {lop.SiSoToiDa - soHocVien} chỗ.");
            }
            else
            {
                Console.WriteLine("Lớp đã đủ sĩ số!");
            }

            Console.ReadKey();
        }

        static void HienThiLopSapKhaiGiang()
        {
            Console.WriteLine("\n===== LỚP SẮP KHAI GIẢNG =====");

            List<ClassRoom> ds = classService.LopSapKhaiGiang();

            if (ds.Count == 0)
            {
                Console.WriteLine("Không có lớp sắp khai giảng.");
                Console.ReadKey();
                return;
            }

            foreach (ClassRoom c in ds)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã lớp          : {c.MaLop}");
                Console.WriteLine($"Tên lớp         : {c.TenLop}");
                Console.WriteLine($"Mã khóa học     : {c.MaKhoaHoc}");
                Console.WriteLine($"Ngày khai giảng : {c.NgayKhaiGiang:dd/MM/yyyy}");
                Console.WriteLine($"Lịch học        : {c.LichHoc}");
                Console.WriteLine($"Sĩ số tối đa    : {c.SiSoToiDa}");
                Console.WriteLine($"Trạng thái      : {c.TrangThai}");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Tổng số lớp sắp khai giảng: {ds.Count}");

            Console.ReadKey();
        }

        static void HienThiLopDangHoc()
        {
            Console.WriteLine("\n===== LỚP ĐANG HỌC =====");

            List<ClassRoom> ds = classService.LopDangHoc();

            if (ds.Count == 0)
            {
                Console.WriteLine("Không có lớp đang học.");
                Console.ReadKey();
                return;
            }

            foreach (ClassRoom c in ds)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã lớp          : {c.MaLop}");
                Console.WriteLine($"Tên lớp         : {c.TenLop}");
                Console.WriteLine($"Mã khóa học     : {c.MaKhoaHoc}");
                Console.WriteLine($"Ngày khai giảng : {c.NgayKhaiGiang:dd/MM/yyyy}");
                Console.WriteLine($"Lịch học        : {c.LichHoc}");
                Console.WriteLine($"Sĩ số tối đa    : {c.SiSoToiDa}");
                Console.WriteLine($"Trạng thái      : {c.TrangThai}");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Tổng số lớp đang học: {ds.Count}");

            Console.ReadKey();
        }
        static void DongLop()
        {
            Console.WriteLine("\n===== ĐÓNG LỚP =====");

            Console.Write("Nhập mã lớp cần đóng: ");
            string maLop = Console.ReadLine();

            ClassRoom? lop = classService.GetById(maLop);

            if (lop == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Mã lớp: {lop.MaLop}");
            Console.WriteLine($"Tên lớp: {lop.TenLop}");
            Console.WriteLine($"Trạng thái hiện tại: {lop.TrangThai}");

            Console.Write("Bạn có chắc muốn đóng lớp này? (Y/N): ");
            string xacNhan = Console.ReadLine();

            if (xacNhan.ToUpper() != "Y")
            {
                Console.WriteLine("Đã hủy thao tác.");
                Console.ReadKey();
                return;
            }

            bool result = classService.DongLop(maLop);

            if (result)
            {
                Console.WriteLine("Đóng lớp thành công!");
                Console.WriteLine($"Trạng thái mới: {lop.TrangThai}");
            }
            else
            {
                Console.WriteLine("Đóng lớp thất bại!");
            }

            Console.ReadKey();
        }

        static void HuyLop()
        {
            Console.WriteLine("\n===== HỦY LỚP =====");

            Console.Write("Nhập mã lớp cần hủy: ");
            string maLop = Console.ReadLine();

            ClassRoom? lop = classService.GetById(maLop);

            if (lop == null)
            {
                Console.WriteLine("Không tìm thấy lớp!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Mã lớp: {lop.MaLop}");
            Console.WriteLine($"Tên lớp: {lop.TenLop}");
            Console.WriteLine($"Trạng thái hiện tại: {lop.TrangThai}");

            Console.Write("Bạn có chắc muốn hủy lớp này? (Y/N): ");
            string xacNhan = Console.ReadLine();

            if (xacNhan.ToUpper() != "Y")
            {
                Console.WriteLine("Đã hủy thao tác.");
                Console.ReadKey();
                return;
            }

            bool result = classService.HuyLop(maLop);

            if (result)
            {
                Console.WriteLine("Hủy lớp thành công!");
                Console.WriteLine($"Trạng thái mới: {lop.TrangThai}");
            }
            else
            {
                Console.WriteLine("Hủy lớp thất bại!");
            }

            Console.ReadKey();
        }
        // CHỨC NĂNG KHÓA HỌC

        static void ThemKhoaHoc()
        {
            Console.Clear();

            Console.WriteLine("===== THÊM KHÓA HỌC =====");

            Course c = new Course();

            Console.Write("Mã khóa học: ");
            c.MaKhoaHoc = Console.ReadLine();

            Console.Write("Tên khóa học: ");
            c.TenKhoaHoc = Console.ReadLine();

            Console.Write("Học phí: ");
            c.HocPhi = decimal.Parse(Console.ReadLine());

            Console.Write("Thời lượng: ");
            c.ThoiLuong = int.Parse(Console.ReadLine());

            Console.Write("Mô tả: ");
            c.MoTa = Console.ReadLine();

            Console.Write("Trạng thái: ");
            c.TrangThai = Console.ReadLine();

            bool result = courseService.Add(c);

            if (result)
                Console.WriteLine("Thêm thành công!");
            else
                Console.WriteLine("Mã khóa học đã tồn tại!");

            Console.ReadKey();
        }

        static void HienThiDanhSach()
        {
            Console.Clear();

            List<Course> ds = courseService.GetAll();

            Console.WriteLine("========================================");
            Console.WriteLine("         DANH SÁCH KHÓA HỌC");
            Console.WriteLine("========================================");

            if (ds.Count == 0)
            {
                Console.WriteLine("Chưa có khóa học nào!");
                Console.ReadKey();
                return;
            }

            foreach (Course c in ds)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Mã khóa học : {c.MaKhoaHoc}");
                Console.WriteLine($"Tên khóa học: {c.TenKhoaHoc}");
                Console.WriteLine($"Học phí     : {c.HocPhi:N0} VNĐ");
                Console.WriteLine($"Thời lượng  : {c.ThoiLuong}");
                Console.WriteLine($"Mô tả       : {c.MoTa}");
                Console.WriteLine($"Trạng thái  : {c.TrangThai}");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Tổng số khóa học: {ds.Count}");

            Console.ReadKey();
        }

        static void SuaKhoaHoc()
        {
            Console.Write("Nhập mã khóa học cần sửa: ");
            string ma = Console.ReadLine();

            Course? course = courseService.GetById(ma);

            if (course == null)
            {
                Console.WriteLine("Không tìm thấy khóa học!");
                Console.ReadKey();
                return;
            }

            Console.Write("Tên khóa học mới: ");
            course.TenKhoaHoc = Console.ReadLine();

            Console.Write("Học phí mới: ");
            course.HocPhi = decimal.Parse(Console.ReadLine());

            Console.Write("Thời lượng mới: ");
            course.ThoiLuong = int.Parse(Console.ReadLine());

            Console.Write("Mô tả mới: ");
            course.MoTa = Console.ReadLine();

            Console.Write("Trạng thái mới: ");
            course.TrangThai = Console.ReadLine();

            Console.WriteLine("Sửa khóa học thành công!");

            Console.ReadKey();
        }

        static void XoaKhoaHoc()
        {
            Console.Write("Nhập mã khóa học cần xóa: ");
            string ma = Console.ReadLine();

            bool result = courseService.Delete(ma);

            if (result)
                Console.WriteLine("Xóa thành công!");
            else
                Console.WriteLine("Không tìm thấy khóa học!");

            Console.ReadKey();
        }

        static void TimKiemKhoaHoc()
        {
            Console.Write("Nhập mã hoặc tên khóa học: ");
            string keyword = Console.ReadLine();

            List<Course> ds =
                courseService.Search(keyword);

            Console.WriteLine();
            Console.WriteLine("===== KẾT QUẢ TÌM KIẾM =====");

            foreach (Course c in ds)
            {
                Console.WriteLine(
                    $"{c.MaKhoaHoc} - " +
                    $"{c.TenKhoaHoc} - " +
                    $"{c.HocPhi:N0} - " +
                    $"{c.TrangThai}");
            }

            Console.ReadKey();
        }

        static void SapXepKhoaHoc()
        {
            List<Course> ds =
                courseService.SortByFee();

            Console.WriteLine();
            Console.WriteLine(
                "===== KHÓA HỌC THEO HỌC PHÍ =====");

            foreach (Course c in ds)
            {
                Console.WriteLine(
                    $"{c.MaKhoaHoc} - " +
                    $"{c.TenKhoaHoc} - " +
                    $"{c.HocPhi:N0}");
            }

            Console.ReadKey();
        }

        static void LocTheoTrangThai()
        {
            Console.Write("Nhập trạng thái cần lọc: ");
            string status = Console.ReadLine();

            List<Course> ds =
                courseService.FilterByStatus(status);

            Console.WriteLine();
            Console.WriteLine("===== KẾT QUẢ =====");

            foreach (Course c in ds)
            {
                Console.WriteLine(
                    $"{c.MaKhoaHoc} - " +
                    $"{c.TenKhoaHoc} - " +
                    $"{c.HocPhi:N0} - " +
                    $"{c.TrangThai}");
            }

            Console.ReadKey();
        }

        static void ThongKeHocPhi()
        {
            decimal tong =
                courseService.GetTotalFee();

            Console.WriteLine();
            Console.WriteLine("===== THỐNG KÊ HỌC PHÍ =====");
            Console.WriteLine(
                $"Tổng học phí: {tong:N0} VNĐ");

            Console.ReadKey();
        }
    }


}